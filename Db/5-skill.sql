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