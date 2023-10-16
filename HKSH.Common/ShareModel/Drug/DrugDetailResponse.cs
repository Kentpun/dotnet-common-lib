namespace HKSH.Common.ShareModel.Drug
{
    /// <summary>
    /// DrugDetailResponse
    /// </summary>
    public class DrugDetailResponse
    {
        /// <summary>
        /// Gets or sets the name of the drug.
        /// </summary>
        /// <value>
        /// The name of the drug.
        /// </value>
        public string DrugName { get; set; } = string.Empty;

        #region drug name

        /// <summary>
        /// Gets or sets the name of the trade.
        /// </summary>
        /// <value>
        /// The name of the trade.
        /// </value>
        public string TradeName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the generic route form.
        /// </summary>
        /// <value>
        /// The generic route form.
        /// </value>
        public string GenericRouteForm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the VMP.
        /// </summary>
        /// <value>
        /// The name of the VMP.
        /// </value>
        public string VmpName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the special status.
        /// </summary>
        /// <value>
        /// The special status.
        /// </value>
        public string SpecialStatus { get; set; } = string.Empty;

        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in house formulary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is in house formulary; otherwise, <c>false</c>.
        /// </value>
        public bool isInHouseFormulary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pca.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is pca; otherwise, <c>false</c>.
        /// </value>
        public bool IsPCA { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is applicable calendar pack.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is applicable calendar pack; otherwise, <c>false</c>.
        /// </value>
        public bool IsApplicableCalendarPack { get; set; }

        /// <summary>
        /// Gets or sets the default prescribing unit.
        /// </summary>
        /// <value>
        /// The default prescribing unit.
        /// </value>
        public string DefaultPrescribingUnit { get; set; } = string.Empty;

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets a value indicating whether [diluent mandatory].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [diluent mandatory]; otherwise, <c>false</c>.
        /// </value>
        public bool DiluentMandatory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dangerous drug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dangerous drug; otherwise, <c>false</c>.
        /// </value>
        public bool IsDangerousDrug { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dosage mandatory].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dosage mandatory]; otherwise, <c>false</c>.
        /// </value>
        public bool DosageMandatory { get; set; }

        /// <summary>
        /// Gets or sets the active whitelist fluid types.
        /// </summary>
        /// <value>
        /// The active whitelist fluid types.
        /// </value>
        public List<DrugIdNameResponse> ActiveWhitelistFluidTypes { get; set; } = new List<DrugIdNameResponse>();

        /// <summary>
        /// Gets or sets the active others fluid types.
        /// </summary>
        /// <value>
        /// The active others fluid types.
        /// </value>
        public List<DrugIdNameResponse> ActiveOthersFluidTypes { get; set; } = new List<DrugIdNameResponse>();

        /// <summary>
        /// Gets or sets the active whitelist routes.
        /// </summary>
        /// <value>
        /// The active whitelist routes.
        /// </value>
        public List<DrugIdNameResponse> ActiveWhitelistRoutes { get; set; } = new List<DrugIdNameResponse>();

        /// <summary>
        /// Gets or sets the active others routes.
        /// </summary>
        /// <value>
        /// The active others routes.
        /// </value>
        public List<DrugIdNameResponse> ActiveOthersRoutes { get; set; } = new List<DrugIdNameResponse>();

        /// <summary>
        /// Gets or sets the dispensing units.
        /// </summary>
        /// <value>
        /// The dispensing units.
        /// </value>
        public List<DrugIdNameResponse> DispensingUnits { get; set; } = new List<DrugIdNameResponse>();

        /// <summary>
        /// Gets or sets a value indicating whether this instance is non drug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is non drug; otherwise, <c>false</c>.
        /// </value>
        public bool IsNonDrug { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bsa based.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is bsa based; otherwise, <c>false</c>.
        /// </value>
        public bool IsBsaBased { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is weight based.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is weight based; otherwise, <c>false</c>.
        /// </value>
        public bool IsWeightBased { get; set; }

        /// <summary>
        /// Gets or sets the common order display.
        /// </summary>
        /// <value>
        /// The common order display.
        /// </value>
        public string CommonOrderDisplay { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the dosage value.
        /// </summary>
        /// <value>
        /// The dosage value.
        /// </value>
        public string DosageValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the dosage unit.
        /// </summary>
        /// <value>
        /// The dosage unit.
        /// </value>
        public string DosageUnit { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        public string Frequency { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the is PRN.
        /// </summary>
        /// <value>
        /// The is PRN.
        /// </value>
        public bool IsPRN { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public string Duration { get; set; } = string.Empty;
    }
}
