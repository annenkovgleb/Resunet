create table if not exists Post(
	PostId serial primary key,
	UserId int, 
	UniqId varchar(250),
	Title varchar(250),
	Intro varchar(250),
	Created timestamp,
	Modified timestamp,
	Status int -- published / not
);

create table if not exists PostContent(
	PostContentId serial primary key,
	PostId int, 
	ContentItemType int, 
	Value text, 
);

create index if not exists IX_Post_UserId on Post(UserId);
create index if not exists IX_PostContent_PostId on PostContent(PostId);
