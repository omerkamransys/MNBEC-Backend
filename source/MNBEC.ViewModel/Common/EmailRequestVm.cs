namespace MNBEC.ViewModel.Common
{
    public class EmailRequestVm : BaseRequestVM
    {
        //[Range(0, 60)]
        public string Name { get; set; }

        // [EmailAddress]
        public string Email { get; set; }

        //  [Range(0, 2000)]
        public string Message { get; set; }
    }
}
