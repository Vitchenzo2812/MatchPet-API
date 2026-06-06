namespace MatchPet.Features;

public interface IBaseHandler<in TRequest>
{
  Task Handle(TRequest request);
}

public interface IBaseHandler<in TRequest, TResponse>
{
  Task<TResponse> Handle(TRequest request);
}