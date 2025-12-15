namespace StockGuard.SharedKernel;

public abstract class Entity
{
	private readonly List<IDomainEvent> _domainEvents = [];

	public List<IDomainEvent> DomainEvents => [.. _domainEvents];

	public void RaiseDomainEvent(IDomainEvent domianEvent)
	{
		_domainEvents.Add(domianEvent);
	}

	public void ClearDomainEvent()
	{
		_domainEvents.Clear();
	}
}

