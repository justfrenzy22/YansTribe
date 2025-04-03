using System.ComponentModel.DataAnnotations;

namespace core.responses
{
    public class BaseRes
    {
        [Display(Name = "exception")]
        public string? exception { get; set; }

        // [Required]
        [Display(Name = "check")]
        public bool check { get; set; }


        public BaseRes()
        {
            check = false;
        }

        // when run into an exception
        public BaseRes(bool check, string? exception = null)
        {
            this.check = check;
            this.exception = exception;
        }
    }
}