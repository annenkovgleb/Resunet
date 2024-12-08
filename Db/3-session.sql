create table if not exists DbSession (
	DbSessionId uuid primary key, -- uuid - 16 byte unique identifier
	SessionData text, -- for saving any data 
	Created timestamp, 
	LastAccessed timestamp,
	UserId int
);