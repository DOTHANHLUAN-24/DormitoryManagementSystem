using System;
using System.Collections.Generic;
using System.Linq;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator.Business
{
    public static class InvoiceDataGenerator
    {
        public static void Generate(SeedContext ctx)
        {
            var contracts = ctx.Contracts.ToList();
            if (!contracts.Any()) return;

            var electricity = ctx.Utilities.FirstOrDefault(u => u.UtilityName.Contains("Điện"));
            var water = ctx.Utilities.FirstOrDefault(u => u.UtilityName.Contains("Nước"));
            var internet = ctx.Utilities.FirstOrDefault(u => u.UtilityName.Contains("Internet"));
            var parking = ctx.Utilities.FirstOrDefault(u => u.UtilityName.Contains("xe"));

            // Thời gian hiện tại
            var now = DateTime.Now;

            // Phát sinh hóa đơn cho 2 tháng gần nhất:
            // - Tháng trước (đã thanh toán)
            // - Tháng này (chưa thanh toán hoặc quá hạn)
            var monthsToGenerate = new List<(int Month, int Year, bool IsPaid)>
            {
                (now.AddMonths(-1).Month, now.AddMonths(-1).Year, true),
                (now.Month, now.Year, false)
            };

            foreach (var contract in contracts)
            {
                var bed = ctx.Beds.FirstOrDefault(b => b.Id == contract.BedId);
                if (bed == null) continue;

                var room = ctx.Rooms.FirstOrDefault(r => r.Id == bed.RoomId);
                if (room == null) continue;

                var roomType = ctx.RoomTypes.FirstOrDefault(rt => rt.Id == room.RoomTypeId);
                decimal roomPrice = roomType?.BasePrice ?? 1200000m;

                foreach (var billPeriod in monthsToGenerate)
                {
                    // Không tạo hóa đơn nếu thời gian phát sinh trước khi hợp đồng bắt đầu
                    var billDate = new DateTime(billPeriod.Year, billPeriod.Month, 1);
                    var contractStartDateStartOfMonth = new DateTime(contract.StartDate.Year, contract.StartDate.Month, 1);
                    if (billDate < contractStartDateStartOfMonth) continue;

                    var invoiceId = Guid.NewGuid();
                    var contractCodeClean = contract.ContractCode.Replace("HD-", "");
                    var invoiceCode = $"HD-{billPeriod.Year}{billPeriod.Month:D2}-{contractCodeClean}";
                    var title = $"Hóa đơn tiền phòng & dịch vụ tháng {billPeriod.Month}/{billPeriod.Year}";

                    var utilityAmount = 0m;
                    var localUtilityUsages = new List<UtilityUsage>();

                    // 1. Điện
                    if (electricity != null)
                    {
                        double prevIdx = (billPeriod.Month - 1) * 150 + 200;
                        double currIdx = prevIdx + ctx.Faker.Random.Number(80, 150);
                        double usage = currIdx - prevIdx;
                        decimal total = (decimal)usage * electricity.UnitPrice;

                        localUtilityUsages.Add(new UtilityUsage
                        {
                            Id = Guid.NewGuid(),
                            RoomId = room.Id,
                            UtilityId = electricity.Id,
                            InvoiceId = invoiceId,
                            Month = billPeriod.Month,
                            Year = billPeriod.Year,
                            PreviousIndex = prevIdx,
                            CurrentIndex = currIdx,
                            UsageQuantity = usage,
                            TotalAmount = total,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = billDate
                        });
                        utilityAmount += total;
                    }

                    // 2. Nước
                    if (water != null)
                    {
                        double prevIdx = (billPeriod.Month - 1) * 10 + 15;
                        double currIdx = prevIdx + ctx.Faker.Random.Number(5, 12);
                        double usage = currIdx - prevIdx;
                        decimal total = (decimal)usage * water.UnitPrice;

                        localUtilityUsages.Add(new UtilityUsage
                        {
                            Id = Guid.NewGuid(),
                            RoomId = room.Id,
                            UtilityId = water.Id,
                            InvoiceId = invoiceId,
                            Month = billPeriod.Month,
                            Year = billPeriod.Year,
                            PreviousIndex = prevIdx,
                            CurrentIndex = currIdx,
                            UsageQuantity = usage,
                            TotalAmount = total,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = billDate
                        });
                        utilityAmount += total;
                    }

                    // 3. Phụ phí
                    var localSurcharges = new List<Surcharge>();
                    var surchargeAmount = 0m;

                    // Internet fee
                    if (internet != null)
                    {
                        localSurcharges.Add(new Surcharge
                        {
                            Id = Guid.NewGuid(),
                            InvoiceId = invoiceId,
                            SurchargeName = "Phí Internet tốc độ cao",
                            Amount = 50000m,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = billDate
                        });
                        surchargeAmount += 50000m;
                    }

                    // Phí gửi xe nếu học sinh có đăng ký xe máy/xe đạp
                    var hasVehicle = ctx.Vehicles.Any(v => v.OwnerId == contract.UserId);
                    if (hasVehicle && parking != null)
                    {
                        localSurcharges.Add(new Surcharge
                        {
                            Id = Guid.NewGuid(),
                            InvoiceId = invoiceId,
                            SurchargeName = "Phí gửi xe tháng",
                            Amount = parking.UnitPrice,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = billDate
                        });
                        surchargeAmount += parking.UnitPrice;
                    }

                    // Phí vệ sinh chung
                    localSurcharges.Add(new Surcharge
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = invoiceId,
                        SurchargeName = "Phí vệ sinh công cộng",
                        Amount = 30000m,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDate = billDate
                    });
                    surchargeAmount += 30000m;

                    var totalInvoiceAmount = roomPrice + utilityAmount + surchargeAmount;

                    var dueDate = new DateTime(billPeriod.Year, billPeriod.Month, 15);
                    var status = billPeriod.IsPaid ? InvoiceStatus.Paid : (now > dueDate ? InvoiceStatus.Overdue : InvoiceStatus.Unpaid);

                    var invoice = new Invoice
                    {
                        Id = invoiceId,
                        ContractId = contract.Id,
                        InvoiceCode = invoiceCode,
                        Title = title,
                        BillingMonth = billPeriod.Month,
                        BillingYear = billPeriod.Year,
                        TotalAmount = totalInvoiceAmount,
                        DueDate = dueDate,
                        Status = status,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDate = billDate
                    };

                    // Nếu đã thanh toán, thêm bản ghi thanh toán tương ứng
                    if (status == InvoiceStatus.Paid)
                    {
                        var paymentDate = new DateTime(billPeriod.Year, billPeriod.Month, ctx.Faker.Random.Number(1, 12));
                        var method = ctx.Faker.PickRandom<PaymentMethod>();
                        var transactionCode = $"GD-{paymentDate:yyyyMMdd}-{ctx.Faker.Random.Number(100000, 999999)}";

                        ctx.Payments.Add(new Payment
                        {
                            Id = Guid.NewGuid(),
                            InvoiceId = invoiceId,
                            AmountPaid = totalInvoiceAmount,
                            PaymentDate = paymentDate,
                            TransactionCode = transactionCode,
                            Method = method,
                            Note = $"Thanh toán cho hóa đơn {invoiceCode}",
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = paymentDate
                        });
                    }

                    ctx.Invoices.Add(invoice);
                    ctx.UtilityUsages.AddRange(localUtilityUsages);
                    ctx.Surcharges.AddRange(localSurcharges);
                }
            }
        }
    }
}
