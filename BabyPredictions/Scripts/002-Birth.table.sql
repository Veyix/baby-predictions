CREATE TABLE IF NOT EXISTS "Birth" (
	Id			int							generated always as identity,
	BirthDate   timestamp with time zone	not null,
    BirthWeight int                         not null,
    Gender      int                         not null,
    CreatedDate timestamp with time zone    not null default now()
);
