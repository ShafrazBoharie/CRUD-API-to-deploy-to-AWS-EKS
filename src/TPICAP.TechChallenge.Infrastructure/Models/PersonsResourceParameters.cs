namespace TPICAP.TechChallenge.Infrastructure.Models
{
    public record PersonsResourceParameters
    {
        private const int maxPageSize = 20;

        private string _orderByString = "FirstName asc";

        private int _pageSize = 10;

        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

        public string OrderBy
        {
            get => _orderByString.Split()[0];
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _orderByString = value;
            }
        }

        public string Fields { get; set; }

        public bool IsAscending
        {
            get
            {
                var orderBySplit = OrderBy.Split();

                if (orderBySplit.Length == 1 || orderBySplit[1].ToLower() == "asc")
                    return true;
                return false;
            }
        }
    }
}