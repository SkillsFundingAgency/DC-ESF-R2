﻿namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class DasLearnerCommitment
    {
        public int Id { get; set; }
        public long Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long AimSeqNumber { get; set; }
        public string CommitmentId { get; set; }
    }
}
