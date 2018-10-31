CREATE TABLE IF NOT EXISTS "Prediction" (
	Id			int							generated always as identity,
	Forename	varchar(512)				not null,
	Surname     varchar(512)       			not null,
	BirthDate   timestamp with time zone	not null,
    BirthWeight int                         not null,
    Gender      int                         not null,
    CreatedDate timestamp with time zone    not null default now()
);