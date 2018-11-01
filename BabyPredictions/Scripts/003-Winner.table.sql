CREATE TABLE IF NOT EXISTS "Winner" (
	Id				int							generated always as identity,
	Forename		varchar(512)				not null,
	Surname     	varchar(512)       			not null,
	Position		int 						not null,
	Points			int							not null,
    CreatedDate 	timestamp with time zone    not null default now()
);