using AutoMapper;
using System;
using System.Globalization;
using System.Linq;
using TechnicalAssignment.Data.Entities;
using TechnicalAssignment.Models;
using TechnicalAssignment.ResponseModels;

namespace TechnicalAssignment.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionModel, XmlTransactionModel>();
            CreateMap<TransactionModel, CsvTransactionModel>()
                .ForMember(x => x.Date,
                    options => options.MapFrom(transaction => DateTime.ParseExact(
                        transaction.Date,
                        "dd/MM/yyyy hh:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None)));

            CreateMap<CsvTransactionModel, Transaction>()
                .ForMember(x => x.StatusId, options =>
                     options.MapFrom(transaction => (int)MapFromCsvStatus(transaction.Status)))
                .ForMember(x => x.Status, options => options.Ignore());

            CreateMap<XmlTransactionModel, Transaction>()
                .ForMember(x => x.StatusId, options =>
                     options.MapFrom(transaction => (int)MapFromXmlStatus(transaction.Status)))
                .ForMember(x => x.Status, options => options.Ignore());

            CreateMap<Transaction, GetTransactionResponseModel>()
                .ForMember(x => x.PaymentDetails, options =>
                      options.MapFrom(transaction => $"{transaction.Amount} {transaction.CurrencyCode}"))
                .ForMember(x => x.Status, options =>
                      options.MapFrom(transaction => ((UnifiedTransactionStatuses)transaction.StatusId).ToString().First()));
        }

        private UnifiedTransactionStatuses MapFromCsvStatus(CsvTransactionStatuses status)
        {
            switch (status)
            {
                case CsvTransactionStatuses.Approved:
                    {
                        return UnifiedTransactionStatuses.Approved;
                    }

                case CsvTransactionStatuses.Failed:
                    {
                        return UnifiedTransactionStatuses.Rejected;
                    }

                case CsvTransactionStatuses.Finished:
                    {
                        return UnifiedTransactionStatuses.Done;
                    }
            }

            throw new ArgumentException("Invalid csv status", nameof(status));
        }

        private UnifiedTransactionStatuses MapFromXmlStatus(XmlTransactionStatuses status)
        {
            switch (status)
            {
                case XmlTransactionStatuses.Approved:
                    {
                        return UnifiedTransactionStatuses.Approved;
                    }

                case XmlTransactionStatuses.Rejected:
                    {
                        return UnifiedTransactionStatuses.Rejected;
                    }

                case XmlTransactionStatuses.Done:
                    {
                        return UnifiedTransactionStatuses.Done;
                    }
            }

            throw new ArgumentException("Invalid csv status", nameof(status));
        }
    }
}
