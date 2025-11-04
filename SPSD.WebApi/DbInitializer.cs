using SPSD.WebApi.Model;

namespace SPSD.WebApi
{
    /// <summary>
    /// 设定数据库种子
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dbContext"></param>
        public static void Initialize(AppDbContext dbContext)
        {
            if (!dbContext.MaterialInfos.Any())
            {
                var materials = new List<MaterialInfo>
                {
                    new MaterialInfo
                    {
                        Id = 1,
                        MaterialName = "钨合金",
                        StrengthModelName = "JOHNSON_COOK",
                        StrengthModelParameter = "RO,G,E,PR,DTF,VP,A,B,N,C,M,TM,TR,EPSO,CP,PC,SPAL,IT,D1,D2,D3,D4,D5",
                        StrengthModelValue = "17.6,1.36,3.5,0.284,,,0.151E-01,0.177E-02,0.12,0.80E-02,1.0,0.1450E+04,294,0.100E-05,0.134E-05,-9.00E+00,3.00,0.0,1.5,0.00,0.00,0.00,0.00",
                        EOSName = "GRUNEISEN",
                        EOSParameter = "C,S1,S2,S3,GAMAO,A,E0,V0",
                        EOSValue = "0.399,1.24,0,0,1.54,0,0,0",
                        ReferenceCount = 0
                    },
                    new MaterialInfo
                    {
                        Id = 2,
                        MaterialName = "45#钢",
                        StrengthModelName = "PLASTIC_KINEMATIC",
                        StrengthModelParameter = "RO,E,PR,SIGY,ETAN,BETA,SRC,SRP,FS,VP",
                        StrengthModelValue = "7.896,2.1,0.284,1.00E-02,,1,,,0.8,0",
                        //EOSName = "空",
                        EOSName = "",
                        EOSParameter = "",
                        EOSValue = "",
                        ReferenceCount = 0
                    },

                     new MaterialInfo
                    {
                        Id = 3,
                        MaterialName = "1",
                        StrengthModelName = "MAT_COMPOSITE_DAMAGE",
                        StrengthModelParameter = "ro,ea,eb,ec,prba,prca,prcb,gab,gbc,gca,kfail,aopt,macf,atrack,,xp,yp,zp,a1,a2,a3,,,v1,v2,v3,d1,d2,d3,beta,,sc,xt,yt,yc,alph,sn,syz,szx",
                        StrengthModelValue = "0.97,1.79,1.79,0.197,0.008,0.044,0.044,0.073,0.067,0.067,0.022,0.0, 1,0,,0.0,0.0,0.0,0.0,0.0,0.0,,,0.0,0.0,0.0,0.0,0.0,0.0,0.0,,0.036,0.03,0.03,0.022,0.5,0.096,0.095,0.0",
                         //EOSName = "空",
                        EOSName = "",
                        EOSParameter = "",
                        EOSValue = "",
                        FailureModelName = "MAT_ADD_EROSION",
                        FailureModelParameter = "excl,mxpres,mneps,effeps,voleps,numfip,ncs,mnpres,sigp1,sigvm,mxeps,epssh,sigth,impulse,failtm,idam,-,-,-,-,-,-,lcregd,lcfld,nsff,epsthin,engcrt,radcrt,lceps12,lceps13,lcepsmx,dteflt,unused,mxtmp,dtmin",
                        FailureModelValue = "0.0,0.0,0.0,0.0,-0.6,1.0,1.0,0.0,0.0,0.0,0.0,0.6,0.0,0.0,0.0,0,0,0,0,0,0,0,0,0,10,0.0,0.0,0.0,0,0,0,0.0,,0.0,0.0",
                        ReferenceCount = 0
                    },
                    new MaterialInfo
                    {
                        Id = 4,
                        MaterialName = "2",
                        StrengthModelName = "MAT_COMPOSITE_DAMAGE",
                        StrengthModelParameter = "ro,ea,eb,ec,prba,prca,prcb,gab,gbc,gca,kfail,aopt,macf,atrack,,xp,yp,zp,a1,a2,a3,,,v1,v2,v3,d1,d2,d3,beta,,sc,xt,yt,yc,alph,sn,syz,szx",
                        StrengthModelValue = "1.60,0.307,0.307,0.0197,0.008,0.044,0.044,0.0073,0.0067,0.0067,0.022,0.0, 1,0,,0.0,0.0,0.0,0.0,0.0,0.0,,,0.0,0.0,0.0,0.0,0.0,0.0,0.0,,0.0036,0.03,0.03,0.022,0.5,0.0096,0.0095,0.0",
                        //EOSName = "空",
                        EOSName = "",
                        EOSParameter = "",
                        EOSValue = "",
                        FailureModelName = "MAT_ADD_EROSION",
                        FailureModelParameter = "excl,mxpres,mneps,effeps,voleps,numfip,ncs,mnpres,sigp1,sigvm,mxeps,epssh,sigth,impulse,failtm,idam,-,-,-,-,-,-,lcregd,lcfld,nsff,epsthin,engcrt,radcrt,lceps12,lceps13,lcepsmx,dteflt,unused,mxtmp,dtmin",
                        FailureModelValue = "0.0,0.0,0.0,0.0,-0.6,1.0,1.0,0.0,0.0,0.0,0.0,0.6,0.0,0.0,0.0,0,0,0,0,0,0,0,0,0,10,0.0,0.0,0.0,0,0,0,0.0,,0.0,0.0",
                        ReferenceCount = 0
                    },

                    new MaterialInfo
                    {
                        Id = 5,
                        MaterialName = "81",
                        StrengthModelName = "MAT_MODIFIED_JOHNSON_COOK_TITLE",
                        StrengthModelParameter = "ro,e,pr,beta,xsi,cp,alpha,e0dot,tr,tm,t0,flag1,flag2,,,a/siga,b/b,n/beta0,c/beta1,m/na,,,,q1/a,c1/n,q2/alpha0,c2/alpha1,,,,,dc/dc,pd/wc,d1/na,d2/na,d3/na,d4/na,d5/na,,tc,tauc",
                        StrengthModelValue = "8.52,1.15,0.21,0.0,0.0,3.85000E-6,0.0,1.00000E-6,298.0,1288.0,295.0,0.0,1.0,,,0.0011169,0.0050469,0.42,0.0085,1.68,,,,0.0,0.0,0.0,0.0,,,,,0.42,0.00914,0.0,0.0,0.0,0.0,0.0,,0.0,0.0",
                         //EOSName = "空",
                        EOSName = "",
                        EOSParameter = "",
                        EOSValue = "",
                        ReferenceCount = 0
                    },
                    new MaterialInfo
                    {
                        Id = 6,
                        MaterialName = "82",
                        StrengthModelName = "MAT_MODIFIED_JOHNSON_COOK_TITLE",//MAT_MODIFIED_JOHNSON_COOK 实际上要去除_TITLE
                        StrengthModelParameter = "ro,e,pr,beta,xsi,cp,alpha,e0dot,tr,tm,t0,flag1,flag2,,,a/siga,b/b,n/beta0,c/beta1,m/na,,,,q1/a,c1/n,q2/alpha0,c2/alpha1,,,,,dc/dc,pd/wc,d1/na,d2/na,d3/na,d4/na,d5/na,,tc,tauc",
                        StrengthModelValue = "10.66,0.16,0.42,0.0,0.0,1.24000E-6,0.0,7.21100E-5,298.0,525.0,295.0,0.0,1.0,,,0.0,2.41100E-4,0.0987,0.1593,1.0,,,,0.0,0.0,0.0,0.0,,,,,0.37,0.00175,0.0,0.0,0.0,0.0,0.0,,0.0,0.0",
                        //EOSName = "空",
                        EOSName = "",
                        EOSParameter = "",
                        EOSValue = "",
                        ReferenceCount = 0
                    },

                     new MaterialInfo
                    {
                        Id = 7,
                        MaterialName = "83",
                        StrengthModelName = "MAT_JOHNSON_COOK",
                        StrengthModelParameter = "ro,g,e,pr,dtf,vp,rateop,a,b,n,c,m,tm,tr,epso,cp,pc,spall,it,d1,d2,d3,d4,d5,c2/p,erod,efmin,numint",
                        StrengthModelValue = "7.83,0.818,0.0,0.3,0.0,0.0,0.0,0.01,0.0051,0.26,0.014,1.03,1793.0,298.0,1.00000E-6,0.00455,0.0,2.0,0.0,0.0,0.33,-1.5,0.0,0.0,0.0,0,1.0,0.0,,",
                        EOSName = "EOS_GRUNEISEN",
                        EOSParameter = "c,s1,s2,s3,gamao,a,e0,v0 ",
                        EOSValue = "0.443,1.33,0.0,0.0,2.47,0.47,0.0,0.0",
                        ReferenceCount = 0
                    },
                     // 失效模型
                    // new MaterialInfo
                    //{
                    //    Id = 8,
                    //    MaterialName = "1",
                    //    StrengthModelName = "MAT_ADD_EROSION",
                    //    StrengthModelParameter = "excl,mxpres,mneps,effeps,voleps,numfip,ncs,mnpres,sigp1,sigvm,mxeps,epssh,sigth,impulse,failtm,idam,-,-,-,-,-,-,lcregd,lcfld,nsff,epsthin,engcrt,radcrt,lceps12,lceps13,lcepsmx,dteflt,unused,mxtmp,dtmin",
                    //    StrengthModelValue = "0.0,0.0,0.0,0.0,-0.6,1.0,1.0,0.0,0.0,0.0,0.0,0.6,0.0,0.0,0.0,0,0,0,0,0,0,0,0,0,10,0.0,0.0,0.0,0,0,0,0.0,,0.0,0.0",
                    //    EOSName = "空",
                    //    EOSParameter = "",
                    //    EOSValue = "",
                    //    ReferenceCount = 0
                    //},

                    // new MaterialInfo
                    //{
                    //    Id = 9,
                    //    MaterialName = "2",
                    //    StrengthModelName = "MAT_ADD_EROSION",
                    //    StrengthModelParameter = "excl,mxpres,mneps,effeps,voleps,numfip,ncs,mnpres,sigp1,sigvm,mxeps,epssh,sigth,impulse,failtm,idam,-,-,-,-,-,-,lcregd,lcfld,nsff,epsthin,engcrt,radcrt,lceps12,lceps13,lcepsmx,dteflt,unused,mxtmp,dtmin",
                    //    StrengthModelValue = "0.0,0.0,0.0,0.0,-0.6,1.0,1.0,0.0,0.0,0.0,0.0,0.6,0.0,0.0,0.0,0,0,0,0,0,0,0,0,0,10,0.0,0.0,0.0,0,0,0,0.0,,0.0,0.0",
                    //    EOSName = "空",
                    //    EOSParameter = "",
                    //    EOSValue = "",
                    //    ReferenceCount = 0
                    //},

                     // ------抗爆模块相关材料start-----------------------

                      new MaterialInfo
                    {
                        Id = 8,
                        MaterialName = "Q235",
                        StrengthModelName = "MAT_PLASTIC_KINEMATIC_TITLE",//MAT_PLASTIC_KINEMATIC 实际上要去除_TITLE
                        StrengthModelParameter = "ro,e,pr,sigy,etan,beta,,src,srp,fs,vp",
                        StrengthModelValue = "7.85,2.0,0.3,0.00355,0.0118,1.0,,0.0,0.0,0.16,0.0",
                        //EOSName = "空",
                        EOSName = "",
                        EOSParameter = "",
                        EOSValue = "",
                        ReferenceCount = 0
                    },

                     new MaterialInfo
                    {
                        Id = 9,
                        MaterialName = "TNT",
                        StrengthModelName = "MAT_HIGH_EXPLOSIVE_BURN",
                        StrengthModelParameter = "ro,d,pcj,beta,k,g,sigy",
                        StrengthModelValue = "1.64,0.693,0.21,0.0,0.0,0.0,0.0",
                        EOSName = "EOS_JWL",
                        EOSParameter = "a,b,r1,r2,omeg,e0,vo",
                        EOSValue = "3.74,0.0323,4.15,0.95,0.3,0.07,1.0",
                        ReferenceCount = 0
                    },

                     // ------抗爆模块相关材料end-----------------------
                };

                dbContext.MaterialInfos.AddRange(materials);
            }

            if (!dbContext.MaterialItems.Any())
            {
                var materialItems = new List<MaterialItem>()
                {
                    new MaterialItem()
                    {
                        Id = 1,
                        MaterialName = "空气",
                        EOSName = "",
                        EOSParameter = "",
                        EOSValue ="",
                        StrengthModelName = "",
                        StrengthModelParameter = "",
                        StrengthModelValue = "",
                    }
                };
                dbContext.MaterialItems.AddRange(materialItems);
            }

            dbContext.SaveChanges();
        }
    }
}