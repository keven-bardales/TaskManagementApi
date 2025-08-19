namespace TaskManagementApi.Features.Tasks.API.Contracts
{
    public class GetTasksQuery
    {
        public bool? IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
