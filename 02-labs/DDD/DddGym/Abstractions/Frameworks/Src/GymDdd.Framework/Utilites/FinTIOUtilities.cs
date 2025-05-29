namespace GymDdd.Framework.Utilites;

public static class FinTIOUtilities
{
    // 일반 값 (예: int, string 등) → FinT<RT, T>로 lift
    public static FinT<IO, C> SelectMany<A, B, C>(
        this A value,
        Func<A, FinT<IO, B>> bind,
        Func<A, B, C> project)
    {
        return FinT<IO, A>.Succ(value)
                          .Bind(a => bind(a).Map(b => project(a, b)));
    }

    //// 기존 FinT<RT, T>에 대한 SelectMany
    //public static FinT<RT, TResult> SelectMany<RT, TSource, TIntermediate, TResult>(
    //    this FinT<RT, TSource> source,
    //    Func<TSource, FinT<RT, TIntermediate>> bind,
    //    Func<TSource, TIntermediate, TResult> project)
    //    where RT : struct, HasIO<RT> =>
    //        source.Bind(t1 =>
    //            bind(t1).Map(t2 => project(t1, t2)));

    ///// <summary>
    ///// Enables LINQ query comprehension by lifting Fin<A> into FinT<IO, A>
    ///// </summary>
    //public static FinT<IO, B> SelectMany<A, B>(
    //    this Fin<A> self,
    //    Func<A, FinT<IO, B>> bind)
    //{
    //    return self.Match(
    //        Succ: a => bind(a),
    //        Fail: err => FinT<IO>.Fail<B>(err)
    //    );
    //}

    //Fin<A>:   Trainer.Create(...)
    //bind:     A → FinT<IO, B>: trainer => AddTrainerAsync(trainer)
    //project:  (A, B) → C: (trainer, _) => unit
    //
    //✅ 성공 흐름(Fin이 Succ일 경우):
    //  - Trainer.Create(...) → Fin<Trainer>.Succ(a)
    //  - bind(a) 실행 → AddTrainerAsync(a) → FinT<IO, Unit>
    //  - .Map(...) 실행 → 결과 타입은 FinT<IO, Unit> 유지됨
    //❌ 실패 흐름(Fin이 Fail일 경우):
    //  - Trainer.Create(...) → Fin<Trainer>.Fail(err)
    //  - FinT<IO>.Fail<C>(err)로 바로 변환 → 이후 바인딩 무시됨

    // -------------------------
    // SelectMany 함수
    // -------------------------
    //Trainer.Create(userId: domainEvent.UserId, id: domainEvent.TrainerId)
    //       .SelectMany(trainer => _trainersRepository.AddTrainerAsync(trainer), (trainer, _) => Prelude.unit);

    // -------------------------
    // 에러
    // -------------------------
    // Cannot implicitly convert
    //      type 'LanguageExt.FinT<LanguageExt.IO, LanguageExt.Unit>'
    //      to 'LanguageExt.Traits.K<LanguageExt.TryT<LanguageExt.Fin>, LanguageExt.Unit>'.
    //      An explicit conversion exists (are you missing a cast?)
    // Cannot implicitly convert
    //      type 'LanguageExt.FinT<LanguageExt.IO, LanguageExt.Unit>'
    //      to 'LanguageExt.Guard<LanguageExt.Common.Error, LanguageExt.Unit>'
    // Argument 1: cannot convert
    //      from 'LanguageExt.Unit'
    //      to 'GymManagement.Domain.AggregateRoots.Trainers.Trainer'


    // Fin<A> → FinT<RT, T>로 lift
    public static FinT<IO, C> SelectMany<A, B, C>(
        this Fin<A> self,
        Func<A, FinT<IO, B>> bind,
        Func<A, B, C> project)
    {
        return self.Match(
            Succ: a =>
                bind(a).Map(b => project(a, b)),
            Fail: err => FinT<IO>.Fail<C>(err)
        );
    }
}