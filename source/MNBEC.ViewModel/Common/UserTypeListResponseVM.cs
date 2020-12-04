namespace MNBEC.ViewModel.Common
{
    /// <summary>
    /// CityListResponseVM class holds basic request properties.
    /// </summary>
    public class UserTypeListResponseVM 
    {
        #region Properties and Data Members
        public uint UserTypeId { get; set; }
        public string UserTypeCode { get; set; }
        public string UserTypeName { get; set; }
        public string UserTypeNameTranslation { get; set; }

        #endregion
    }
}