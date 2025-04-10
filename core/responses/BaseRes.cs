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

        [Display(Name = "status")]
        public int status { get; set; }

        public BaseRes()
        {
            check = false;
        }

        // when run into an exception
        public BaseRes(bool check, int status, string? exception = null)
        {
            this.check = check;
            this.status = status;
            this.exception = exception;
        }
    }
}