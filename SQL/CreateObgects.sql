if db_id('ObjectsLifeCycle') is null
	Create database ObjectsLifeCycle
go

use ObjectsLifeCycle
go

-- Тег:
if object_id('tag') is null
	create table tag(
		  tagId numeric(15,0) identity
	    , tagNum varchar(50)
		, dateInstall smalldatetime
		primary key (tagId)
	)

-- Группа прочности:
if object_id('strength') is null
create table strength(
	  strengthId numeric(15,0) identity
	, [name] varchar(50)
	primary key (strengthId)
)

-- Стандартная длина:
if object_id('standardLen') is null
create table standardLen(
	  standardLenId numeric(15,0) identity
	, lengthVal float
	primary key (standardLenId)
)

-- Тип трубы:
if object_id('pipeType') is null
create table pipeType(
	  pipeTypeId numeric(15,0) identity
	, pipeTypeVal varchar(20)
	primary key (pipeTypeId)
)

-- Диаметр:
if object_id('pipeDiameter') is null
create table pipeDiameter(
	  pipeDiameterId numeric(15,0) identity
	, pipeDiameterVal varchar(20)
	primary key (pipeDiameterId)
)

-- Межнипельное покрытие:
if object_id('betwHipple') is null
create table betwHipple(
	  betwHippleId numeric(15,0) identity
	, brief varchar(25)
	, discript varchar(100)
	primary key (betwHippleId)
)

-- Фирма:
if object_id('firm') is null
create table firm(
	  firmId numeric(15,0) identity
	, brief varchar(25)
	, [name] varchar(50)
	primary key (firmId)
)

-- Защита:
if object_id('protection') is null
create table protection(
	  protectionId numeric(15,0) identity
	, brief varchar(25)
	primary key (protectionId)
)


-- Объект, на который накладывается условие:
if object_id('caseObject') is null
create table caseObject(
	  caseObjectId numeric(15,0) identity
    , caseObjectValue varchar(50)  --объект, на который накладывается условие
	primary key (caseObjectId)
)

-- Тип условия (больше, меньше):
if object_id('caseType') is null
create table caseType(
	  caseTypeId numeric(15,0) identity
    , caseTypeValue varchar(50)
	primary key (caseTypeId)
)

-- Сравниваемая величина:
if object_id('caseResult') is null
create table caseResult(
	  caseResultId numeric(15,0) identity
    , caseResultValue varchar(50)
	primary key (caseResultId)
)

-- Внутреннее покрытие:
if object_id('intercoating') is null
create table intercoating(
	  intercoatingId numeric(15,0) identity
    , techCase varchar(50)  --ТУ
	, thickness float       --толщина
	, color varchar(20)     --цвет
    , firmId numeric(15,0)  --фирма
	primary key (intercoatingId)
)

-- Дополнительные условия для внутреннего покрытия:
if object_id('intercoatingCaseRelation') is null
create table intercoatingCaseRelation(
	  intercoatingCaseRelationId numeric(15,0) identity
	, intercoatingId numeric(15,0) --id внутреннего покрытия
	, caseObjectId numeric(15,0)   --объект, на который накладывается условие
	, caseTypeId numeric(15,0)     --тип условия (больше, меньше)
	, caseResultId numeric(15,0)   --сравниваемая величина
	  primary key (intercoatingCaseRelationId)
	, FOREIGN KEY (intercoatingId) REFERENCES intercoating(intercoatingId)
	, FOREIGN KEY (caseObjectId) REFERENCES caseObject(caseObjectId)
	, FOREIGN KEY (caseTypeId) REFERENCES caseType(caseTypeId)
	, FOREIGN KEY (caseResultId) REFERENCES caseResult(caseResultId)
)

-- Документ (сертификат):
if object_id('document') is null
create table document(
	  documentId numeric(15,0) identity
	, objectID numeric(15,0)  -- ID Объекта, к которому относится сертифика
    , [name] varchar(50)      -- имя файла
	, type int                -- тип документа 1/2 : труба/покрытие внутреннее
    , addedDate smalldatetime -- дата добавления в систему
	, patch varchar(100)      -- каталог расположение
	primary key (documentId)
)

-- Муфта:
if object_id('coupling') is null
create table coupling(
	  couplingId numeric(15,0) identity
    , batchNum numeric(15,0)    -- номер партии
	, smeltingNum numeric(15,0) -- номер плавки

	, pipeDiameterId numeric(15,0)
	, protectionId numeric(15,0)
	, strengthId numeric(15,0)
	primary key (couplingId)
	, FOREIGN KEY (pipeDiameterId) REFERENCES pipeDiameter(pipeDiameterId)
	, FOREIGN KEY (protectionId) REFERENCES protection(protectionId)
	, FOREIGN KEY (strengthId) REFERENCES strength(strengthId)
)

-- Труба:
if object_id('pipe') is null
create table pipe(
	  pipeId numeric(15,0) identity
    , pipeNum numeric(15,0)
	, factoryNum numeric(15,0)   -- заводской номер
	, batchNum numeric(15,0)     -- номер партии
	, smeltingNum numeric(15,0)  -- номер плавки
    , gostThCon varchar(50)      -- гост ТУ
	, packageNum varchar(50)     -- номер пакета
	, releaseDate smalldatetime  -- дата выпуска
	, outerCoating int default 0 -- внешнее наружнее покрытие (признак 1/0 есть/нет)
	, carving int default 0      -- резьба(признак 1/0 есть/нет)
	, [certificate] varchar(50)  -- сертификат
	, otk varchar(50)            -- информация об ОТК

	, strengthId numeric(15,0)     -- группа прочности
	, standardLenId numeric(15,0)  -- стандартная длина
	, pipeTypeId numeric(15,0)     -- тип трубы
	, tagId numeric(15,0)          -- метка
	, couplingId numeric(15,0)     -- муфта
	, intercoatingId numeric(15,0) -- Внутреннее покрытие
	, betwHippleId numeric(15,0)   -- Межнипельное покрытие:
	  
	  primary key (pipeId)
	, FOREIGN KEY (strengthId) REFERENCES strength(strengthId)
	, FOREIGN KEY (standardLenId) REFERENCES standardLen(standardLenId)
	, FOREIGN KEY (pipeTypeId) REFERENCES pipeType(pipeTypeId)
	, FOREIGN KEY (couplingId) REFERENCES coupling(couplingId)
	, FOREIGN KEY (tagId) REFERENCES tag(tagId)
	, FOREIGN KEY (intercoatingId) REFERENCES intercoating(intercoatingId)
	, FOREIGN KEY (betwHippleId) REFERENCES betwHipple(betwHippleId)

)

-- Типичные операции:
if object_id('typicalOperation') is null
create table typicalOperation(
	  typicalOperationId numeric(15,0) identity
    , value varchar(100)
	primary key (typicalOperationId)
)

-- Пользователь:
if object_id('employee') is null
create table employee(
	  employeeId numeric(15,0) identity
	, name varchar(30) 
	, patronymic varchar(30) 
	, surname varchar(30) 
	, access int --1/2 admin/просмотр
	, userlogin varchar(30) 
	, userPassword varchar(30) 
	, isActive int default 1 --1/0 активный/не активный
	primary key (employeeId)
)

-- Жизненный цикл:
if object_id('lifeCycle') is null
create table lifeCycle(
	  lifeCycleId numeric(15,0) identity
	, lifeCycleDateAdded smalldatetime default getDate()
	, comment varchar(200)
	, geoposition varchar(50)

	, tagId numeric(15,0)              -- метка
	, typicalOperationId numeric(15,0) -- операция
	, firmId numeric(15,0)
	, employeeId numeric(15,0) --toDo переделать на сервер
	primary key (lifeCycleId)
	, FOREIGN KEY (tagId) REFERENCES tag(tagId)
	, FOREIGN KEY (typicalOperationId) REFERENCES typicalOperation(typicalOperationId)
	, FOREIGN KEY (firmId) REFERENCES firm(firmId)
	, FOREIGN KEY (employeeId) REFERENCES employee(employeeId)
)
--================View:
if object_id('v_Document') is not null
drop VIEW v_Document
go
CREATE VIEW v_Document AS   
with documentsCte(Documenttype, docPipName, addedDate, patch, tagId) as
(
	select Documenttype = 'Труба'
	     , docPipName = docPip.name
		 , addedDate = docPip.addedDate
		 , patch = docPip.patch
		 , tagId = pip.tagId
    from pipe pip
   inner join document docPip on docPip.objectId = pip.pipeId and docPip.type = 1 --pipe
   union all
	select Documenttype = 'Внутреннее покрытие'
	     , docPipName = docInter.name
		 , addedDate = docInter.addedDate
		 , patch = docInter.patch
		 , tagId = pip.tagId
      from pipe pip
   inner join document docInter on docInter.objectId = pip.intercoatingId and docInter.type = 2 --intercoating
)
   select ROW_NUMBER() OVER(ORDER BY Documenttype ASC) AS num, * 
     from documentsCte
go

if object_id('v_LifeCicle') is not null
drop VIEW v_LifeCicle
go
CREATE VIEW v_LifeCicle AS   
select lifeCycleId = lc.lifeCycleId
     , dateAdded = lc.lifeCycleDateAdded
	 , operationName = toper.value
	 , firmName = fi.brief
	 , fio = emp.surname + ' ' + LEFT(emp.name,1) + '. ' + LEFT(emp.patronymic,1) + '.'
	 , geoposition = lc.geoposition
	 , tagId = lc.tagId
  from lifeCycle lc
 inner join typicalOperation toper on toper.typicalOperationId = lc.typicalOperationId
 inner join employee emp on emp.employeeId = lc.employeeId
 inner join firm fi on fi.firmId = lc.firmId
go

if object_id('v_mainInf') is not null
drop VIEW v_mainInf
go
CREATE VIEW v_mainInf AS   
   select tagId = t.tagId
        , pipeNum = pip.pipeNum
		, factoryNum = pip.factoryNum
		, batchNum = pip.batchNum
		, smeltingNum = pip.smeltingNum
		, gostThCon = pip.gostThCon
		, packageNum = pip.packageNum
		, releaseDate = pip.releaseDate
		, certificate = pip.[certificate]
		, otk = pip.otk
        , yeReleasDate = year(pip.releaseDate) 
		, monReleasDate = month(pip.releaseDate) 
		, dayReleasDate = day(pip.releaseDate) 
        , yeDateInstall = year(t.dateInstall) 
		, monDateInstall = month(t.dateInstall) 
		, dayDateInstall = day(t.dateInstall) 
        , typeDiametr = pipeTyp.pipeTypeId
		, strengthId = stren.strengthId
		, standardLenId = standartLe.standardLenId
        , carving = pip.carving
		, isCoupling = isnull(coup.couplingId,0)
		, isOuterCoating = pip.outerCoating
		, isIntercoating = isnull(intercoat.intercoatingId,0)
		, isBetwHipple = isnull( betw.betwHippleId,0)
		, tagNum = t.tagNum
   from pipe pip
  inner join strength stren on  stren.strengthId = pip.strengthId
  inner join standardLen standartLe on  standartLe.standardLenId = pip.standardLenId
  inner join pipeType pipeTyp on  pipeTyp.pipeTypeId = pip.pipeTypeId
  left join coupling coup on  coup.couplingId = pip.couplingId
  inner join tag t on  t.tagId = pip.tagId
  left join intercoating intercoat on  intercoat.intercoatingId = pip.intercoatingId
  left join betwHipple betw on betw.betwHippleId = pip.betwHippleId
go

if object_id('v_CouplingDetail') is not null
drop VIEW v_CouplingDetail
go
CREATE VIEW v_CouplingDetail AS   
select strenName = stren.name
     , pipeDiameterVal = diam.pipeDiameterVal
	 , batchNum = coup.batchNum
	 , smeltingNum = coup.smeltingNum
	 , protectionBrief = prot.brief
	 , tagId = pip.tagId
  from pipe pip
 inner join coupling coup on coup.couplingId = pip.couplingId
 inner join protection prot on prot.protectionId = coup.protectionId
 inner join pipeDiameter diam on diam.pipeDiameterId = coup.pipeDiameterId
 inner join strength stren on stren.strengthId = coup.strengthId
go

if object_id('v_IntercoatingDetail') is not null
drop VIEW v_IntercoatingDetail
go
CREATE VIEW v_IntercoatingDetail AS   
  select firmBrief = f.brief
       , firmName = f.name
	   , techCase = ic.techCase
	   , thickness = ic.thickness
	   , color = ic.color
	   , tagId = pip.tagId
    from pipe pip
   inner join intercoating ic on ic.intercoatingId = pip.intercoatingId
   inner join firm f on f.firmId = ic.firmId

go

if object_id('v_caseObjectDeail') is not null
drop VIEW v_caseObjectDeail
go
CREATE VIEW v_caseObjectDeail AS   
  select caseObjectValue = co.caseObjectValue
       , caseTypeValue = ct.caseTypeValue
	   , caseResultValue = cr.caseResultValue
	   , tagId = pip.tagId
    from pipe pip
   inner join intercoating ic on ic.intercoatingId = pip.intercoatingId
   inner join intercoatingCaseRelation icr on icr.intercoatingId = ic.intercoatingId
   inner join caseObject co on co.caseObjectId = icr.caseObjectId
   inner join caseType ct on ct.caseTypeId = icr.caseTypeId
   inner join caseResult cr on cr.caseResultId = icr.caseResultId
go

if object_id('v_BetwHippleDeail') is not null
drop VIEW v_BetwHippleDeail
go
CREATE VIEW v_BetwHippleDeail AS   
  select betwHippleBrief = betw.brief
       , betwHippleDiscript = betw.discript
	   , tagId = pip.tagId
    from pipe pip
   inner join betwHipple betw on betw.betwHippleId = pip.betwHippleId
go


--================Заполнение тестовыми данными:
-- Тег:
if not exists(select 1 from tag where tagNum = 'E2003A33D5297889349F9AA6')
	insert into tag(tagNum, dateInstall) select 'E2003A33D5297889349F9AA6', '20200112'

-- Группа прочности:
if not exists(select 1 from strength where [name] = 'Д')
	insert into strength([name]) select 'Д'
if not exists(select 1 from strength where [name] = 'Д(А)')
	insert into strength([name]) select 'Д(А)'
if not exists(select 1 from strength where [name] = 'Д(Б)')
	insert into strength([name]) select 'Д(Б)'

-- Стандартная длина:
if not exists(select 1 from standardLen where lengthVal = 9.5)
	insert into standardLen(lengthVal) select 9.5
if not exists(select 1 from standardLen where lengthVal = 6)
	insert into standardLen(lengthVal) select 6
if not exists(select 1 from standardLen where lengthVal = 7)
	insert into standardLen(lengthVal) select 7

-- Тип трубы:
if not exists(select 1 from pipeType where pipeTypeVal = 'НКТ 73x5.5')
	insert into pipeType(pipeTypeVal) select 'НКТ 73x5.5'
if not exists(select 1 from pipeType where pipeTypeVal = 'НКТ Ø60')
	insert into pipeType(pipeTypeVal) select 'НКТ Ø60'

-- Межнипельное покрытие:
if not exists(select 1 from betwHipple where brief = 'Русма-1')
	insert into betwHipple(brief, discript) select 'Русма-1', 'Свободные резьбовые части ниппеля и муфты покрыты смазкой'

-- Диаметр:
if not exists(select 1 from pipeDiameter where pipeDiameterVal = '93.17')
	insert into pipeDiameter(pipeDiameterVal) select '93.17'

-- Фирма:
if not exists(select 1 from firm where brief = 'MajorCrack')
begin
	insert into firm(brief, [name]) select 'MajorCrack', 'MSPLAG17'
	insert into firm(brief, [name]) select 'ОМК', 'ОМК'
end
	

-- Защита:
if not exists(select 1 from protection where brief = 'фосфатирование')
	insert into protection(brief) select 'фосфатирование'

-- Документ (сертификат):
if not exists(select 1 from document)
begin
	insert into document(objectID, [name], type, addedDate, patch)
	select objectID = 1
	     , [name] = 'Sertifikat_kachestva_postavka_05_05_2019g.pdf'
		 , type = 1 --труба
		 , addedDate = '20200115'
		 , patch = 'D:\Git\Sharp\ObjectsLifeCycle\Certificate'
	union all
	select objectID = 1
	     , [name] = 'Sertifikat_kachestchva_Sinara.pdf'
		 , type = 2 --внутреннее покрытие
		 , addedDate = '20200118'
		 , patch = 'D:\Git\Sharp\ObjectsLifeCycle\Certificate'
end


-- Муфта:
if not exists(select 1 from coupling)
	insert into coupling(batchNum, smeltingNum, pipeDiameterId, protectionId, strengthId) 
	select 9812, 739111, 1,1,1

-- Объект, на который накладывается условие:
if not exists(select 1 from caseObject where caseObjectId = 1)
begin
	insert into caseObject(caseObjectValue) select 'Диэлектр сплошн, кВ/мм'
	insert into caseObject(caseObjectValue) select 'Адгезия, Мпа'
end

-- Тип условия (больше, меньше):
if not exists(select 1 from caseType where caseTypeId = 1)
	insert into caseType(caseTypeValue) select 'Не менее'

-- Сравниваемая величина:
if not exists(select 1 from caseResult where caseResultId = 1)
begin
	insert into caseResult(caseResultValue) select '5'
	insert into caseResult(caseResultValue) select '10'
end

-- Внутреннее покрытие:
if not exists(select 1 from intercoating where intercoatingId = 1)
	insert into intercoating(techCase, thickness, color, firmId)
	select techCase  = '22.20.43-007-69730070-2018' --ТУ
	     , thickness = '115'
		 , color     = 'белый'
		 , firmId    = 1

-- Дополнительные условия для внутреннего покрытия:
if not exists(select 1 from intercoatingCaseRelation where intercoatingCaseRelationId = 1)
begin
	insert into intercoatingCaseRelation(intercoatingId, caseObjectId, caseTypeId, caseResultId) select 1, 1, 1, 1
	insert into intercoatingCaseRelation(intercoatingId, caseObjectId, caseTypeId, caseResultId) select 1, 1, 1, 1
end

-- Труба:
if not exists(select 1 from pipe where pipeId = 1)
	insert into pipe(pipeNum, factoryNum, batchNum, smeltingNum, gostThCon, packageNum, releaseDate, [certificate], otk, outerCoating, carving, strengthId, standardLenId, pipeTypeId, couplingId, tagId, intercoatingId, betwHippleId ) 
	select pipeNum = 123456789
	     , factoryNum = 204187
		 , batchNum = 32605
		 , smeltingNum = 1825846
		 , gostThCon = '633-80'
		 , packageNum = 'А39035'
		 , releaseDate = '20181026'
		 , [certificate] = 'НК 733536/ 06'
		 , otk = 'какая то информация об ОТК'
		 , outerCoating = 1
		 , carving = 1

		 , strengthId = 1
		 , standardLenId = 1
		 , pipeTypeId = 1
		 , couplingId = 1
		 , tagId = 1
		 , intercoatingId = 1
		 , betwHippleId = 1

-- Пользователь:
if not exists(select 1 from employee)
begin
	insert into employee(surname, name, patronymic,  access, userlogin, userPassword) select 'Иванов', 'Иван', 'Иванович', 1, 'admin', 'admin'
	insert into employee(surname, name, patronymic,  access, userlogin, userPassword) select 'Петров', 'Петр', 'Петрович', 1, 'watch', 'watch'
end

-- Типичные операции:
if not exists(select 1 from typicalOperation)
begin
	insert into typicalOperation(value) 
	select 'Изготовление НКТ'
	union all select 'Нанесение резьбы'
	union all select 'Защита резьбы'
	union all select 'Нанесение покрытия'
	union all select 'Отправка трубы Заказчику'
end

-- Жизненный цикл:
if not exists(select 1 from lifeCycle)
begin
	insert into lifeCycle(comment, geoposition, tagId, typicalOperationId, firmId, employeeId) 
	          select 'Комментарий к операции', '', 1, 1, 2, 1
	union all select 'Комментарий к операции', '', 1, 2, 2, 1
	union all select 'Комментарий к операции', '', 1, 3, 2, 1
	union all select 'Комментарий к операции', '', 1, 4, 2, 1

	update lifeCycle set lifeCycleDateAdded = dateadd(day, typicalOperationId, lifeCycleDateAdded)
end


		 
/*
select * from tag
select * from strength
select * from standardLen
select * from pipeType
select * from pipeDiameter
select * from intercoating
select * from firm
select * from protection
select * from document
select * from coupling
select * from pipe

select * from caseObject
select * from caseType
select * from caseResult
select * from intercoating
select * from intercoatingCaseRelation

select * from employee
select * lifeCycle
select * typicalOperation

*/


  /*

 */
--==============================================
   
   /*
   use master
   go
   drop database ObjectsLifeCycle
   */
