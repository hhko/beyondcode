﻿namespace Crop.Hello.Framework.Contracts.Errors;

partial record class Error
{
	public static readonly Error ValidationError = new($"{nameof(ValidationError)}", "A validation problem occurred.");

	public static readonly Error NullValue = new($"{nameof(NullValue)}", "The result value is null.");

	public static readonly Error ConditionNotSatisfied = new($"{nameof(ConditionNotSatisfied)}", "The specified condition was not satisfied.");

	///// <summary>
	///// Create an Error based on the entity type name and the id that was not found
	///// </summary>
	///// <param name="name">name of the entity type. Use "nameof(TValue)" syntax</param>
	///// <param name="id">id of the entity that was not found</param>
	///// <returns>NotFound error</returns>
	//public static Error NotFound<TEntity>(IEntityId id)
	//	where TEntity : class, IEntity
	//{
	//	return New($"{typeof(TEntity).Name}.{nameof(NotFound)}", $"{typeof(TEntity).Name} with id '{id.Value}' was not found.");
	//}

	///// <summary>
	///// Create an NotFount Error based on the entity type name and the unique key
	///// </summary>
	///// <param name="name">name of the entity type. Use "nameof(TValue)" syntax</param>
	///// <param name="key">Key of the entity that was not found</param>
	///// <returns>NotFound error</returns>
	//public static Error NotFound<TEntity>(IUniqueKey key)
	//	where TEntity : class, IEntity
	//{
	//	return New($"{typeof(TEntity).Name}.{nameof(NotFound)}", $"{typeof(TEntity).Name} with key '{key}' was not found.");
	//}

	///// <summary>
	///// Create an NotFound Error based on the entity type name and the unique value
	///// </summary>
	///// <param name="name">name of the entity type. Use "nameof(TValue)" syntax</param>
	///// <param name="uniqueValue">unique value of the entity that was not found</param>
	///// <returns>NotFound error</returns>
	//public static Error NotFound<TEntity>(string uniqueValue)
	//	where TEntity : class, IEntity
	//{
	//	return New($"{typeof(TEntity).Name}.{nameof(NotFound)}", $"{typeof(TEntity).Name} for '{uniqueValue}' was not found.");
	//}

	///// <summary>
	///// Create an NotFound Error based on the entity type name and the unique value
	///// </summary>
	///// <param name="name">name of the entity type. Use "nameof(TValue)" syntax</param>
	///// <param name="uniqueValue">unique value of the entity that was not found</param>
	///// <returns>NotFound error</returns>
	//public static Error NotFound(string subjectToFind, string uniqueValue, string additionalMessage)
	//{
	//	return New($"{subjectToFind}.{nameof(NotFound)}", $"{subjectToFind} for '{uniqueValue}' was not found. {additionalMessage}");
	//}

	///// <summary>
	///// Create an Error based on the unique key
	///// </summary>
	///// <param name="key">unique key of the entity that is already in the database</param>
	///// <returns>AlreadyExists error</returns>
	//public static Error AlreadyExists<TUniqueKey>(TUniqueKey key)
	//	where TUniqueKey : IUniqueKey
	//{
	//	return New($"{typeof(TUniqueKey).Name}.{nameof(AlreadyExists)}", $"{typeof(TUniqueKey).Name} '{key}' already exists.");
	//}

	///// <summary>
	///// Create an Error based on the unique key
	///// </summary>
	///// <param name="key">unique key of the entity that is already in the database</param>
	///// <returns>AlreadyExists error</returns>
	//public static Error AlreadyExists<TEntity>(string key)
	//{
	//	return New($"{typeof(TEntity).Name}.{nameof(AlreadyExists)}", $"{typeof(TEntity).Name} '{key}' already exists.");
	//}

	///// <summary>
	///// Create an Error based on the unique key
	///// </summary>
	///// <param name="key">unique key of the entity that is already in the database</param>
	///// <returns>AlreadyExists error</returns>
	//public static Error AlreadyExists(string subject, string key)
	//{
	//	return New($"{subject}.{nameof(AlreadyExists)}", $"{subject} '{key}' already exists.");
	//}

	///// <summary>
	///// Create an Error describing the NullReference
	///// </summary>
	///// <returns>NullReference error</returns>
	//public static Error NullReference(string subject)
	//{
	//	return New($"{subject}.{nameof(NullReference)}", $"{subject} is null.");
	//}

	///// <summary>
	///// Create an Error describing that the provided reference is invalid
	///// </summary>
	///// <returns>InvalidReference error</returns>
	//public static Error InvalidReference(Ulid reference, string entity)
	//{
	//	return New($"{entity}.{nameof(InvalidReference)}", $"Invalid reference '{reference}' for entity '{entity}'.");
	//}

	///// <summary>
	///// Create an Error describing that the provided references are invalid
	///// </summary>
	///// <returns>InvalidReferences error</returns>
	//public static Error InvalidReferences(IList<Ulid> references, string entity)
	//{
	//	return New($"{entity}.{nameof(InvalidReferences)}", $"Invalid references [{string.Join(", ", references)}] for entity '{entity}'.");
	//}

	///// <summary>
	///// Create an Error describing that the collection is null or empty
	///// </summary>
	///// <returns>NullOrEmpty error</returns>
	//public static Error NullOrEmpty(string collectionName)
	//{
	//	return New($"{collectionName}.{nameof(NullOrEmpty)}", $"{collectionName} is null or empty.");
	//}

	///// <summary>
	///// Create an Error describing that the give case is NotSupported
	///// </summary>
	///// <returns>NotSupported error</returns>
	//public static Error NotSupported(string message)
	//{
	//	return New($"{nameof(NotSupported)}", message);
	//}

	///// <summary>
	///// Create an Error describing that the batch command is invalid
	///// </summary>
	///// <returns>InvalidBatchCommand error</returns>
	//public static Error InvalidBatchCommand(string batchCommand)
	//{
	//	return New($"{batchCommand}.{nameof(InvalidBatchCommand)}", $"{batchCommand} is invalid.");
	//}

	///// <summary>
	///// Create an Error describing that the invalid operation was invoked
	///// </summary>
	///// <returns>InvalidOperation error</returns>
	//public static Error InvalidOperation(string message)
	//{
	//	return New($"{nameof(InvalidOperation)}", message);
	//}

	///// <summary>
	///// Create an Error describing that the invalid argument was provided
	///// </summary>
	///// <returns>InvalidOperation error</returns>
	//public static Error InvalidArgument(string message)
	//{
	//	return New($"{nameof(InvalidArgument)}", message);
	//}

	///// <summary>
	///// Create an Error describing that the verification failed
	///// </summary>
	///// <returns>VerificationError error</returns>
	//public static Error VerificationError(string message)
	//{
	//	return New($"{nameof(VerificationError)}", message);
	//}

	///// <summary>
	///// Create an Error describing that the request is duplicated (same key)
	///// </summary>
	///// <returns>DuplicatedRequest error</returns>
	//public static Error DuplicatedRequest<TBusinessKey>(TBusinessKey key)
	//	where TBusinessKey : IUniqueKey
	//{
	//	return New($"{nameof(DuplicatedRequest)}", $"Duplicated request for key '{key}'.");
	//}

	///// <summary>
	///// Create an Error from the thrown exception
	///// </summary>
	///// <param name="exceptionMessage">Exception message</param>
	///// <returns>Exception error</returns>
	//public static Error Exception(string exceptionMessage)
	//{
	//	return New($"{nameof(Exception)}", exceptionMessage);
	//}

	///// <summary>
	///// Create an Error describing that value was not parsed properly
	///// </summary>
	///// <param name="errorMessage">Exception message</param>
	///// <returns>ParseFailure error</returns>
	//public static Error ParseFailure<ParseType>(string valueParsedName)
	//{
	//	return New($"{nameof(ParseFailure)}", $"Parsing '{valueParsedName}' to type '{nameof(ParseType)}' failed.");
	//}
}
