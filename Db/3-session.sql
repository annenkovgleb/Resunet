create table if not exists DbSession (
	DbSessionId uuid primary key, -- uuid - 16 байтный уникальный индификатор
	SessionData text, -- будем сохранять любые данные
	Created timestamp, -- когда создана
	LastAccessed timestamp, -- когда добирались, для того, чтобы увидеть сессию
	UserId int
)