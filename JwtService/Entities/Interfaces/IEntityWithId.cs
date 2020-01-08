namespace JwtService.Entities.Interfaces
{
    interface IEntityWithId : IEntity
    {
        long ID { get; }
    }
}
