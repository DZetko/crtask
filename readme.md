# CODERAMA assignment

There are a handful of approaches to dealing with this assignment.

Most importantly, storing an **arbitrarily** structured object in a database is tricky and  the database-of-choice can/might be a document database, rather than a relational store. Although, document databases might come at a performance and a cost penalty, it still makes sense given the dynamic nature of the Data key in the Document model. Nevertheless, SQL Server or PostgreSQL allow storing JSON in NVARCHAR(MAX) columns and that's considered a valid approach.

Furthermore, the Tags property is something that a document database would store in a single key-value (array) entry. A relational database offers two options - 1. storing the tags in a Tags column in the Document table but this makes the data denormalized and 2. (as implmented) storing tags in a separate table with a foreign key to the parent Document.

To be able to replace the underlying storage, I abstracted away the data store manipulation using a repository (IDocumentRepository). Replacing the underlying storage is a matter of changing the implementation of IDocumentRepository in the DI container in Program.cs and properly registering other required services.

To improve on the performance requirements, I split the service concerns into queries and commands and implemented the queries using a direct SQL connection. This reduces the overhead of EF Core when performing read queries at a cost of losing some of the type safety. When our domain model changes, our repository logic still points to the old structure. Nevertheless, the domain model changes less frequently and the application maintainer’s responsibility is to follow the business rules.

The implementation would also benefit from a separation of Data entities and Domain entites.

Another possible improvement would rely in the domain layer throwing domain-specific exceptions rather than general .NET exceptions.