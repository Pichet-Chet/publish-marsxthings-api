using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarsXApps.Models.Customs.CAEC
{
	public class MachinesMasterModel
	{
		public MachinesMasterModel()
		{
		}

        public string mcSerialNo { get; set; } = null!;

        public string mcChassis { get; set; } = null!;

        public string mcEngine { get; set; } = null!;

        public string? mcModelGobal { get; set; }

        public string? mcModel { get; set; }

        public int? unit { get; set; }

        public string? ourCompanyName { get; set; }

        public DateTime? portReceiveDate { get; set; }

        public DateTime? branchReceiveDate { get; set; }

        public string? customerName { get; set; }

        public string? branch { get; set; }

        public string? machineStatus { get; set; }

        public decimal? workHrMachine { get; set; }

        public string? colorStatus { get; set; }

        public string? appearance { get; set; }

        public string? cab { get; set; }

        public string? wheelType { get; set; }

        public string? linePipe { get; set; }

        public string? bucketType { get; set; }

        public string? bucketSize { get; set; }

        public decimal? track { get; set; }

        public decimal? weight { get; set; }

        public string? specialParts { get; set; }

        public string? toolbox { get; set; }

        public DateTime? gpsDate { get; set; }

        public string? gpsVendor { get; set; }

        public DateTime? filmDate { get; set; }

        public DateTime? scrapeSerialDate { get; set; }

        public string? saleName { get; set; }

        public string? saleStatus { get; set; }

        public DateTime? outMachineDate { get; set; }

        public string? desination { get; set; }

        public string? remark { get; set; }

        public DateTime? createDate { get; set; }

        public string? createUser { get; set; }

        public DateTime? updateDate { get; set; }

        public string? updateUser { get; set; }

        public string? machineSold { get; set; }

        public string? reasonMachine { get; set; }

        public string? color { get; set; }

        public string? invoiceNo { get; set; }

        public string? customerId { get; set; }

        public int machineId { get; set; }

        public string? branchCode { get; set; }

        public string? brand { get; set; }

        public string? company_owner_name { get; set; }

        public string? owner_mc_status { get; set; }

        public string? carNo { get; set; }

        public decimal? boomLength { get; set; }

        public string? billingNo { get; set; }

        public string? projectName { get; set; }

        public bool? flagGps { get; set; }

        public bool? flagFilms { get; set; }

        public string? companyCode { get; set; }

        public string? locationType { get; set; }

        public string? contractNo { get; set; }

        public string? licensePlate { get; set; }

        public DateTime? registrationDate { get; set; }

        public DateTime? taxExpiryDate { get; set; }

        public string? rentalStatus { get; set; }

        public string? rentalRemark { get; set; }

        public string? insuranceProvider { get; set; }

        public string? insurancePolicyNumber { get; set; }

        public DateTime? insuranceExpiryDate { get; set; }

        public string? machineActProvider { get; set; }

        public string? machineActPolicyNumber { get; set; }

        public DateTime? machineActExpiryDate { get; set; }

        public string? safetyNumber { get; set; }

        public DateTime? safetyExpiryDate { get; set; }

        [NotMapped]
        public string? machineTransactionName { get; set; }

        [NotMapped]
        public string? machineTransactionRemark { get; set; }

        [NotMapped]
        public bool isReceiveMachine { get; set; } = false;

        [NotMapped]
        public decimal? price { get; set; }

        [NotMapped]
        public string? priceRemark { get; set; }

        [NotMapped]
        public string? machineGroup { get; set; }
    }
}

