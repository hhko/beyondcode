using Crop.Hello.Framework.Contracts.Results;
using MediatR;

namespace Crop.Hello.Framework.Contracts.CQRS;

// 결과 값이 있을 때
// - 변경 전: MediatR 원본
//   public interface IRequest<out TResponse> : IBaseRequest { }
// - 변경 후:
//   public interface IQuery<out TResponse> : IRequest<IResult<TResponse>> where TResponse : IResponse { }

public interface IQuery<out TResponse> : IRequest<IResult<TResponse>>
    where TResponse : IResponse
{
}