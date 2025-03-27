using System.ComponentModel.DataAnnotations;

namespace DeltaCare.Entity.Model
{
    public class TDModel : RequestMode
    {
        public int TD_ID { get; set; }
        public string? TEST_ID { get; set; }
        public string? CT { get; set; }
        //public int DTNO { get; set; }
        public string? TCODE { get; set; }
        public string? BCODE { get; set; }
        //public string? HSTNO { get; set; }
        //public string? PTN { get; set; }
        //public string? HOSTCODE { get; set; }
        //public string? IFLG { get; set; }
        //public string? B_NO { get; set; }
        public string? SYNM { get; set; }
        //public string? MTHD { get; set; }
        public string? STATUS { get; set; }
        public string? ORDABLE { get; set; }
        [Display(Name = "FullName")]
        public string? FULL_NAME { get; set; }
        [Display(Name = "AFullName")]
        public string? AFULL_NAME { get; set; }
        public string? UNITS { get; set; }
        public decimal DEC { get; set; }
        public decimal CNVTFCTR { get; set; }
        public string? CNVTCODE { get; set; }
        public string? UC { get; set; }
        public string? CNVUNITS { get; set; }
        public decimal CNVTDEC { get; set; }
        public string? AC { get; set; }
        public string? MDL { get; set; }
        public string? RSTP { get; set; }
        public string? SEQ { get; set; }
        public string? S_TYPE { get; set; }
        public string? SR { get; set; }
        public string? SC { get; set; }
        public string? RES_CODE { get; set; }
        public string? RESULT { get; set; }
        public string? DELTATP { get; set; }
        public decimal DAYSVAL { get; set; }
        public decimal DELTAVAL { get; set; }
        public string? DIV { get; set; }
        public string? SECT { get; set; }
        public string? WC { get; set; }
        public string? TS { get; set; }
        public string? PRFX { get; set; }
        public string? STS { get; set; }
        public string? PRTY { get; set; }
        public string? RR { get; set; }
        public string? AR { get; set; }
        public string? MHN { get; set; }
        public string? SHN { get; set; }
        public decimal TAT { get; set; }
        public string? CTAT { get; set; }
        public string? BILL_NAME { get; set; }
        public string? BILL { get; set; }
        public string? DSCNTG { get; set; }
        public string? DT { get; set; }
        public decimal DSCNT { get; set; }
        public decimal DPRICE { get; set; }
        public decimal UPRICE { get; set; }
        public decimal? UPRICE2 { get; set; }
        public decimal UCOST { get; set; }
        public string? S { get; set; }
        public string? BILL_GRP { get; set; }
        //public string? STK_CODE { get; set; }
        public string? TNO { get; set; }
        public string? RES_TMPLT { get; set; }
        public string? MNOTES { get; set; }
        public string? FNOTES { get; set; }
        public string? MINTERP { get; set; }
        public string? FINTERP { get; set; }
        public bool UPDT { get; set; }
        public string? COL_CNDN { get; set; }
        public string? TEST_INF { get; set; }
        //public string? VLM_MTRL { get; set; }
        //public string? NRML_RNG { get; set; }
        //public string? METHOD { get; set; }
        public string? LBL_CMNT { get; set; }
        //public string? B_NO1 { get; set; }
        //public string? GTDYN { get; set; }
        public bool S_RPT { get; set; }
        public string? PR { get; set; }
        public decimal TOPIC_ID { get; set; }

        //public TDModel(int DTNO_, string? TCODE_, string? BCODE_, string? HSTNO_, string? PTN_, string? HOSTCODE_, string? IFLG_, string? B_NO_, string? SYNM_, string? MTHD_, string? STATUS_, string? ORDABLE_, string? FULL_NAME_, string? AFULL_NAME_, string? UNITS_, decimal DEC_, decimal CNVTFCTR_, string? CNVTCODE_, string? UC_, string? CNVUNITS_, decimal CNVTDEC_, string? AC_, string? MDL_, string? RSTP_, string? SEQ_, string? S_TYPE_, string? SR_, string? SC_, string? RES_CODE_, string? RESULT_, string? DELTATP_, decimal DAYSVAL_, decimal DELTAVAL_, string? DIV_, string? SECT_, string? WC_, string? TS_, string? PRFX_, string? STS_, string? PRTY_, string? RR_, string? AR_, string? MHN_, string? SHN_, decimal TAT_, string? CTAT_, string? BILL_NAME_, string? BILL_, string? DSCNTG_, string? DT_, decimal DSCNT_, decimal UPRICE_, string? S_, string? BILL_GRP_, string? STK_CODE_, string? TNO_, string? RES_TMPLT_, string? MNOTES_, string? FNOTES_, string? MINTERP_, string? FINTERP_, bool UPDT_, string? COL_CNDN_, string? TEST_INF_, string? VLM_MTRL_, string? NRML_RNG_, string? METHOD_, string? FREQUENCY_, string? B_NO1_, string? GTDYN_, bool S_RPT_, string? PR_, decimal TOPIC_ID_)
        //{
        //    this.DTNO = DTNO_;
        //    this.TCODE = TCODE_;
        //    this.BCODE = BCODE_;
        //    this.HSTNO = HSTNO_;
        //    this.PTN = PTN_;
        //    this.HOSTCODE = HOSTCODE_;
        //    this.IFLG = IFLG_;
        //    this.B_NO = B_NO_;
        //    this.SYNM = SYNM_;
        //    this.MTHD = MTHD_;
        //    this.STATUS = STATUS_;
        //    this.ORDABLE = ORDABLE_;
        //    this.FULL_NAME = FULL_NAME_;
        //    this.AFULL_NAME = AFULL_NAME_;
        //    this.UNITS = UNITS_;
        //    this.DEC = DEC_;
        //    this.CNVTFCTR = CNVTFCTR_;
        //    this.CNVTCODE = CNVTCODE_;
        //    this.UC = UC_;
        //    this.CNVUNITS = CNVUNITS_;
        //    this.CNVTDEC = CNVTDEC_;
        //    this.AC = AC_;
        //    this.MDL = MDL_;
        //    this.RSTP = RSTP_;
        //    this.SEQ = SEQ_;
        //    this.S_TYPE = S_TYPE_;
        //    this.SR = SR_;
        //    this.SC = SC_;
        //    this.RES_CODE = RES_CODE_;
        //    this.RESULT = RESULT_;
        //    this.DELTATP = DELTATP_;
        //    this.DAYSVAL = DAYSVAL_;
        //    this.DELTAVAL = DELTAVAL_;
        //    this.DIV = DIV_;
        //    this.SECT = SECT_;
        //    this.WC = WC_;
        //    this.TS = TS_;
        //    this.PRFX = PRFX_;
        //    this.STS = STS_;
        //    this.PRTY = PRTY_;
        //    this.RR = RR_;
        //    this.AR = AR_;
        //    this.MHN = MHN_;
        //    this.SHN = SHN_;
        //    this.TAT = TAT_;
        //    this.CTAT = CTAT_;
        //    this.BILL_NAME = BILL_NAME_;
        //    this.BILL = BILL_;
        //    this.DSCNTG = DSCNTG_;
        //    this.DT = DT_;
        //    this.DSCNT = DSCNT_;
        //    this.UPRICE = UPRICE_;
        //    this.S = S_;
        //    this.BILL_GRP = BILL_GRP_;
        //    this.STK_CODE = STK_CODE_;
        //    this.TNO = TNO_;
        //    this.RES_TMPLT = RES_TMPLT_;
        //    this.MNOTES = MNOTES_;
        //    this.FNOTES = FNOTES_;
        //    this.MINTERP = MINTERP_;
        //    this.FINTERP = FINTERP_;
        //    this.UPDT = UPDT_;
        //    this.COL_CNDN = COL_CNDN_;
        //    this.TEST_INF = TEST_INF_;
        //    this.VLM_MTRL = VLM_MTRL_;
        //    this.NRML_RNG = NRML_RNG_;
        //    this.METHOD = METHOD_;
        //    this.FREQUENCY = FREQUENCY_;
        //    this.B_NO1 = B_NO1_;
        //    this.GTDYN = GTDYN_;
        //    this.S_RPT = S_RPT_;
        //    this.PR = PR_;
        //    this.TOPIC_ID = TOPIC_ID_;
        //}
    }


    public class TD_GTDModel
    {
        //[Key]
        public int GTD_ID { get; set; }
        public string GTNO { get; set; }
        public string GRP_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string TCODE { get; set; }
        public string FULL_NAME { get; set; }
        public string PNDG { get; set; }
        public string GP { get; set; }
        public int? TD_ID { get; set; }
        public string? TEST_ID { get; set; }
        public string? CT { get; set; }
        //public string DTNO { get; set; }
        public string BCODE { get; set; }
        //public string HSTNO { get; set; }
        //public string PTN { get; set; }
        //public string HOSTCODE { get; set; }
        //public string IFLG { get; set; }
        //public string B_NO { get; set; }
        public string SYNM { get; set; }
        //public string MTHD { get; set; }
        public string STATUS { get; set; }
        public string ORDABLE { get; set; }
        public string AFULL_NAME { get; set; }
        public string UNITS { get; set; }
        public decimal? DEC { get; set; }
        public decimal? CNVTFCTR { get; set; }
        public string CNVTCODE { get; set; }
        public string UC { get; set; }
        public string CNVUNITS { get; set; }
        public decimal? CNVTDEC { get; set; }
        public string AC { get; set; }
        public string MDL { get; set; }
        public string RSTP { get; set; }
        public string SEQ { get; set; }
        public string S_TYPE { get; set; }
        public string SR { get; set; }
        public string SC { get; set; }
        public string RES_CODE { get; set; }
        public string RESULT { get; set; }
        public string DELTATP { get; set; }
        public decimal? DAYSVAL { get; set; }
        public decimal? DELTAVAL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string PRFX { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string RR { get; set; }
        public string AR { get; set; }
        public string MHN { get; set; }
        public string SHN { get; set; }
        public decimal? TAT { get; set; }
        public string CTAT { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL { get; set; }
        public string DSCNTG { get; set; } = "ss";
        public string DT { get; set; }
        public decimal? DSCNT { get; set; }
        public decimal? UPRICE { get; set; }
        public string S { get; set; }
        public string BILL_GRP { get; set; }
        //public string STK_CODE { get; set; }
        public string TNO { get; set; }
        public string RES_TMPLT { get; set; }
        public string MNOTES { get; set; }
        public string FNOTES { get; set; }
        public string MINTERP { get; set; }
        public string FINTERP { get; set; }
        //public bool UPDT { get; set; }
        public string COL_CNDN { get; set; }
        public string TEST_INF { get; set; }
        //public string VLM_MTRL { get; set; }
        //public string NRML_RNG { get; set; }
        //public string METHOD { get; set; }
        public string LBL_CMNT { get; set; }
        //public string B_NO1 { get; set; }
        //public string GTDYN { get; set; }
        //public bool S_RPT { get; set; }
        public string PR { get; set; }
        public decimal? TOPIC_ID { get; set; }

        /*
         SELECT        g.GTD_ID, g.GTNO, g.GRP_NO, g.REQ_CODE, g.TCODE, g.FULL_NAME, g.PNDG, g.GP, g.SEQ, t.TD_ID, t.DTNO, t.BCODE, t.HSTNO, t.PTN, t.HOSTCODE, t.IFLG, t.B_NO, t.SYNM, t.MTHD, t.STATUS, t.ORDABLE, t.AFULL_NAME, 
                         t.UNITS, t.DEC, t.CNVTFCTR, t.CNVTCODE, t.UC, t.CNVUNITS, t.CNVTDEC, t.AC, t.MDL, t.RSTP, t.S_TYPE, t.SR, t.SC, t.RES_CODE, t.RESULT, t.DELTATP, t.DAYSVAL, t.DELTAVAL, t.DIV, t.SECT, t.WC, t.TS, t.PRFX, t.STS, t.PRTY, 
                         t.RR, t.AR, t.MHN, t.SHN, t.TAT, t.CTAT, t.BILL_NAME, t.BILL, t.DSCNTG, t.DT, t.DSCNT, t.UPRICE, t.S, t.BILL_GRP, t.STK_CODE, t.TNO, t.RES_TMPLT, t.MNOTES, t.FNOTES, t.MINTERP, t.FINTERP, t.UPDT, t.COL_CNDN, 
                         t.TEST_INF, t.VLM_MTRL, t.NRML_RNG, t.METHOD, t.FREQUENCY, t.B_NO1, t.GTDYN, t.S_RPT, t.PR, t.TOPIC_ID
        FROM            dbo.GTD AS g INNER JOIN
                         dbo.TD AS t ON t.TCODE = g.TCODE AND t.DTNO IS NOT NULL
         */

    }

    public class TD_GTModel
    {
        [Key]
        public int TD_ID { get; set; }
        public string DTNO { get; set; }
        public string TCODE { get; set; }
        public string GRP_NO { get; set; }
        public string B_NO { get; set; }
        public string GRP_NAME { get; set; }
        public string AGRP_NAME { get; set; }
        public string SYNM { get; set; }
        public string RPT { get; set; }
        public string SUBHDR { get; set; }
        public string RPT_NO { get; set; }
        public string SRRR { get; set; }
        public string GP { get; set; }
        public bool? DWL { get; set; }
        public string BCODE { get; set; }
        public string HSTNO { get; set; }
        public string PTN { get; set; }
        public string HOSTCODE { get; set; }
        public string IFLG { get; set; }
        public string MTHD { get; set; }
        public string STATUS { get; set; }
        public string ORDABLE { get; set; }
        public string FULL_NAME { get; set; }
        public string AFULL_NAME { get; set; }
        public string UNITS { get; set; }
        public decimal? DEC { get; set; }
        public decimal? CNVTFCTR { get; set; }
        public string CNVTCODE { get; set; }
        public string UC { get; set; }
        public string CNVUNITS { get; set; }
        public decimal? CNVTDEC { get; set; }
        public string AC { get; set; }
        public string MDL { get; set; }
        public string RSTP { get; set; }
        public string SEQ { get; set; }
        public string S_TYPE { get; set; }
        public string SR { get; set; }
        public string SC { get; set; }
        public string RES_CODE { get; set; }
        public string RESULT { get; set; }
        public string DELTATP { get; set; }
        public decimal? DAYSVAL { get; set; }
        public decimal? DELTAVAL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string PRFX { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string RR { get; set; }
        public string AR { get; set; }
        public string MHN { get; set; }
        public string SHN { get; set; }
        public decimal? TAT { get; set; }
        public string CTAT { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL { get; set; }
        public string DSCNTG { get; set; }
        public string DT { get; set; }
        public decimal? DSCNT { get; set; }
        public decimal? UPRICE { get; set; }
        public string S { get; set; }
        public string BILL_GRP { get; set; }
        public string STK_CODE { get; set; }
        public string TNO { get; set; }
        public string RES_TMPLT { get; set; }
        public string MNOTES { get; set; }
        public string FNOTES { get; set; }
        public string MINTERP { get; set; }
        public string FINTERP { get; set; }
        public bool? UPDT { get; set; }
        public string COL_CNDN { get; set; }
        public string TEST_INF { get; set; }
        public string VLM_MTRL { get; set; }
        public string NRML_RNG { get; set; }
        public string METHOD { get; set; }
        public string FREQUENCY { get; set; }
        public string B_NO1 { get; set; }
        public string GTDYN { get; set; }
        public bool? S_RPT { get; set; }
        public string PR { get; set; }
        public decimal? TOPIC_ID { get; set; }
    }

    public class TestDirectoryModel : RequestMode
    {
        public int? sno { get; set; }
        public int TD_ID { get; set; }
        public string? TEST_ID { get; set; }
        public string? CT { get; set; }
        public string TCODE { get; set; }
        //public string? DTNO { get; set; }
        public string? Full_Name { get; set; }
        public string? AFull_Name { get; set; }
        public string? Synm { get; set; }
        public string? S_TYPE { get; set; }
        public string? UNITS { get; set; }
        public string? PRTY { get; set; }
        public string? Status { get; set; }
        public string? Ordable { get; set; }
        public string? PR { get; set; }
        public string? MDL { get; set; }
        public string RSTP { get; set; }
        public decimal? DEC { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string PRFX { get; set; }
        public string TNO { get; set; }
        public string MHN { get; set; }
        public string SHN { get; set; }
        public string? SEQ { get; set; }
        public string? CNVUNITS { get; set; }
        public decimal? CNVTFCTR { get; set; }
        public decimal? CNVTDEC { get; set; }
        public string? RESULT { get; set; }
        public decimal? TAT { get; set; }
        //public string? PTN { get; set; }
        public string? DELTATP { get; set; }
        public decimal? DELTAVAL { get; set; }
        public decimal? DAYSVAL { get; set; }
        public string? BILL { get; set; }
        public string? BILL_NAME { get; set; }
        public string? DSCNTG { get; set; }
        public decimal? UPRICE { get; set; }
        public decimal? UPRICE2 { get; set; }
        public decimal? UCOST { get; set; }
        public string? MNOTES { get; set; }
        public string? FNOTES { get; set; }
        public string? STS { get; set; }
        public string? MINTERP { get; set; }
        public string? FINTERP { get; set; }        
        public string? TEST_INF { get; set; }
    }

    public class PriceMasterListModel : RequestMode
    {   
        public int TD_ID { get; set; }
        public string BILL { get; set; }
        public decimal UPRICE { get; set; }
        public decimal UPRICE2 { get; set; }    
        
    }

    public class TDDivision
    {
        public string? DIV { get; set; }
        public string? DIVDESC { get; set; }
        public string? SECT { get; set; }
        public string? SECTDESC { get; set; }
    }

    public class TDReferenceRangeModel
    {
        public int REFID { get; set; }
        public string? SITE_NO { get; set; }
        public int DTNO { get; set; }
        public string? TCODE { get; set; }
        public string? RSTP { get; set; }
        public string? S_TYPE { get; set; }
        public string? SEX { get; set; }
        public decimal AGE_F { get; set; }
        public string? AFF { get; set; }
        public decimal AGE_T { get; set; }
        public string? ATF { get; set; }
        public decimal AGE_FROM { get; set; }
        public decimal? AGE_TO { get; set; }
        public decimal? REF_LOW { get; set; }
        public decimal? REF_HIGH { get; set; }
        public decimal? CRTCL_LOW { get; set; }
        public decimal? CRTCL_HIGH { get; set; }
        public string? LHF { get; set; }
        public string? RESPONSE { get; set; }
        public decimal DEC { get; set; }
        public string? REF_RANGE { get; set; }
        public string? REF_LC { get; set; }
        public string? REF_HC { get; set; }
        public string? REMARKS { get; set; }
    }

    public class Response
    {
        public string? messages { get; set; }
        public int responsecode { get; set; }
    }

    public class TDGTDModel : RequestMode
    {
        public int RNO { get; set; }
        public int GTD_ID { get; set; }
        public string GTNO { get; set; }
        public string GRP_NO { get; set; }
        public string REQ_CODE { get; set; }
        public string DTNO { get; set; }
        public string TCODE { get; set; }
        public string FULL_NAME { get; set; }
        public string PNDG { get; set; }
        public string S_TYPE { get; set; }
        public string MDL { get; set; }
        public string RSTP { get; set; }
        public string S { get; set; }
        public string GP { get; set; }
        public string SEQ { get; set; }
    }
    public class TDProfileModel : RequestMode
    {
        public int SNO { get; set; }
        public int TD_ID { get; set; }
        public string TEST_ID { get; set; }
        public string CT { get; set; }
        public string GTNO { get; set; }
        public string TCODE { get; set; }
        public string BCODE { get; set; }
        public string SYNM { get; set; }
        public string STATUS { get; set; }
        public string ORDABLE { get; set; }
        public string FULL_NAME { get; set; }
        public string AFULL_NAME { get; set; }
        public string UNITS { get; set; }
        public decimal DEC { get; set; }
        public decimal CNVTFCTR { get; set; }
        public string CNVTCODE { get; set; }
        public string UC { get; set; }
        public string CNVUNITS { get; set; }
        public decimal CNVTDEC { get; set; }
        public string AC { get; set; }
        public string MDL { get; set; }
        public string RSTP { get; set; }
        public string SEQ { get; set; }
        public string S_TYPE { get; set; }
        public string SR { get; set; }
        public string SC { get; set; }
        public string RES_CODE { get; set; }
        public string RESULT { get; set; }
        public string DELTATP { get; set; }
        public decimal DAYSVAL { get; set; }
        public decimal DELTAVAL { get; set; }
        public string DIV { get; set; }
        public string SECT { get; set; }
        public string WC { get; set; }
        public string TS { get; set; }
        public string PRFX { get; set; }
        public string STS { get; set; }
        public string PRTY { get; set; }
        public string RR { get; set; }
        public string AR { get; set; }
        public string MHN { get; set; }
        public string SHN { get; set; }
        public decimal TAT { get; set; }
        public string CTAT { get; set; }
        public string BILL_NAME { get; set; }
        public string BILL { get; set; }
        public string DSCNTG { get; set; }
        public string DT { get; set; }
        public decimal DSCNT { get; set; }
        public decimal UPRICE { get; set; }
        public decimal UCOST { get; set; }
        public string S { get; set; }
        public string BILL_GRP { get; set; }
        public string TNO { get; set; }
        public string RES_TMPLT { get; set; }
        public string MNOTES { get; set; }
        public string FNOTES { get; set; }
        public string MINTERP { get; set; }
        public string FINTERP { get; set; }
        public bool UPDT { get; set; }
        public string COL_CNDN { get; set; }
        public string TEST_INF { get; set; }
        public string LBL_CMNT { get; set; }
        public bool S_RPT { get; set; }
        public string PR { get; set; }
        public decimal TOPIC_ID { get; set; }


    }
}