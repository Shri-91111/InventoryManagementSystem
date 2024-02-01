namespace IMS.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ItemWorkViewModel
    {
        public string? Id { get; set; }
        public string? Desc { get; set; }
        public int? Typeid { get; set; }
        public int? vendorid { get; set; }
        public int? numbersofquantity { get; set; }
        public string? purchasedate { get; set; }
        public string? throghid { get; set; }

    }
    public class IssuedMasterViewModel
    {
        public string? ProductName { get; set; }
        public string? DeptType { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string? Rol { get; set; }
        public int? Id { get; set; }
        public string? issuedperson { get; set; }
        public string? issuedpersondes { get; set; }
        public string? remainingquantity { get; set; }
    }
    public class IssuedReportsViewModel
    {
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public string? CategoryName { get; set; }

        public List<IssuedMasterList>? IssuedMasterList { get; set; }
    }

    public class IssuedMasterList
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public DateTime? IssuedDateTime { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? PriceDecimal { get; set; }
        public string? Designation { get; set; }

    }

    public class EmployeeReportsViewModel: IssuedReportsViewModel
    {
        public string? employeecode { get; set; }
       
    }
}


