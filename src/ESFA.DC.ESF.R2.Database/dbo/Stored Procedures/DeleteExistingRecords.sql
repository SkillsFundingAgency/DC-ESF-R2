create procedure [dbo].[DeleteExistingRecords] (
	@ukprn int,
	@conRefNum varchar(20)
) as
begin
	delete sdu
	from dbo.SupplementaryDataUnitCost sdu
		inner join dbo.SupplementaryData sd 
			on sdu.ConRefNumber = sd.ConRefNumber 
			and sdu.DeliverableCode = sd.DeliverableCode
			and sdu.CostType = sd.CostType
			and sdu.CalendarYear = sd.CalendarYear
			and sdu.CalendarMonth = sd.CalendarMonth
			and sdu.ReferenceType = sd.ReferenceType
			and sdu.Reference = sd.Reference
		inner join dbo.SourceFile sf on sf.SourceFileId = sd.SourceFileId
	where sf.UKPRN = @ukprn and sf.ConRefNumber = @conRefNum;

	delete sd
	from dbo.SupplementaryData sd
		inner join dbo.SourceFile sf on sf.SourceFileId = sd.SourceFileId
	where sf.UKPRN = @ukprn and sf.ConRefNumber = @conRefNum;
	
	delete ve
	from dbo.ValidationError ve
		inner join dbo.SourceFile sf on sf.SourceFileId = ve.SourceFileId
	where sf.UKPRN = @ukprn and sf.ConRefNumber = @conRefNum;
    
	--delete from dbo.SourceFile where UKPRN = @ukprn and ConRefNumber = @conRefNum;
end