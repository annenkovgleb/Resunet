create table if not exists AppUser (
	UserId serial primary key,
	Email varchar(50),
	Password varchar(100),
	Salt varchar(50),
	Status int
);

create table if not exists UserSecurity (
	UserSecurityId serial primary key, 
	UserId int,
	VerificationCode varchar(50)
);


create table if not exists EmailQueue (
	EmailQueueId serial primary key, 
	EmailTo varchar(200),
	EmailFrom varchar(200),
	EmailSubject varchar(200),
	EmailBody text,
	Created time,
	ProcessingId varchar(100),
	Retry int
);

create index if not exists IX_AppUser_Email on AppUser(
	Email
);

alter table AppUser drop if exists FirstName;
alter table AppUser drop if exists LastName;
alter table AppUser drop if exists ProfileImage;
create table if not exists Profile 
(
	ProfileID serial primary key,
	UserId int, 
	ProfileName varchar(50),
	FirstName varchar(50),
	LastName varchar(50),
	ProfileImage varchar(100)
);

alter table Profile add if not exists ProfileStatus int not null default 0;
create table if not exists DbSession (
	DbSessionId uuid primary key, -- uuid - 16 byte unique identifier
	SessionData text,
	Created timestamp, 
	LastAccessed timestamp,
	UserId int
);
create table if not exists UserToken (
	UserTokenId uuid primary key,
	UserId int,
	Created timestamp
);
create table if not exists Skill (
	SkillId serial primary key,
	SkillName varchar(50)
);

create table if not exists ProfileSkill (
	ProfileSkillId serial primary key,
	ProfileId int, 
	SkillId int,
	Level int
);

create index if not exists IX_ProfileSkill_ProfileId on ProfileSkill (
	ProfileId
);
create table if not exists Post(
	PostId serial primary key,
	UserId int, 
	UniqId varchar(250),
	Title varchar(250),
	Intro varchar(250),
	Created timestamp,
	Modified timestamp,
	Status int
);

create table if not exists PostContent(
	PostContentId serial primary key,
	PostId int, 
	ContentItemType int, 
	Value text
);

create index if not exists IX_Post_UserId on Post(UserId);
create index if not exists IX_PostContent_PostId on PostContent(PostId);

