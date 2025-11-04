namespace SPSD.WebApi.ParamModel
{
    public class PaginationQueryModel
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class PaginationResultModel<T>
    {

        public List<T>? List { get; set; }
        public int PageCount { get; set; }
    }


    public class PaginationQueryMaterialItemModel : PaginationQueryModel
    { 
        public int ProjectId { get; set; }
    }
}
