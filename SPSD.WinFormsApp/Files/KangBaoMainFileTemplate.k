*KEYWORD
*NODE
 1000001             0.0             0.0             0.0       0       0
 1000002             0.0             0.0             0.0       0       0
 1000003             1.0             0.0             0.0       0       0
 1000004             0.0             1.0             0.0       0       0
*CONTROL_ALE
$      DCT      NADV      METH      AFAC      BFAC      CFAC      DFAC      EFAC
$#     dct      nadv      meth      afac      bfac      cfac      dfac      efac
        -1         1         3      -1.0       0.0       0.0       0.0       0.0
$#   start       end     aafac     vfact      prit       ebc      pref   nsidebc
       0.01.00000E20       1.01.00000E-6         0         0       0.0         0
$#    ncpl      nbkt    imascl    checkr    beamin   mmgpref    pdifmx   dtmufac
         1        50         0       0.0       0.0         0       0.0       0.0
$# optimpp    ialedr    bndflx    minmas      
         0         0         01.00000E-5
*CONTROL_BULK_VISCOSITY
$       Q1        Q2      TYPE     BTYPE
$#      q1        q2      type     btype    tstype      
       1.5      0.06         1         0         0
*CONTROL_CONTACT
$   SLSFAC    RWPNAL    ISLCHK    SHLTHK    PENOPT    THKCHG     ORIEN    ENMASS
$#  slsfac    rwpnal    islchk    shlthk    penopt    thkchg     orien    enmass
{{slsfac}}       0.0         1         0         1         0         1         0
$   USRSTR    USRFRC     NSBCS    INTERM     XPENE     SSTHK      ECDT   TIEDPRJ
$#  usrstr    usrfrc     nsbcs    interm     xpene     ssthk      ecdt   tiedprj
         0         0         0         0       4.0         0         0         0
$    SFRIC     DFRIC       EDC       VFC        TH     TH_SF    PEN_SF     PTSCL
$#   sfric     dfric       edc       vfc        th     th_sf    pen_sf      
       0.0       0.0       0.0       0.0       0.0       0.0       0.0
$   IGNORE    FRCENG   SKIPRWG    OUTSEG   SPOTSTP   SPOTDEL   SPOTHIN
$#  ignore    frceng   skiprwg    outseg   spotstp   spotdel   spothin       
         0         0         0         0         0         0       0.0
$     ISYM    NSEROD    RWGAPS    RWGDTH     RWKSF      ICOV    SWRADF    ITHOFF
$#    isym    nserod    rwgaps    rwgdth     rwksf      icov    swradf    ithoff
         0         0         1       0.0       1.0         0       0.0         0
$   SHLEDG    PSTIFF    ITHCNT    TDCNOF     FTALL              SHLTRW    IGACTC
$#  shledg    pstiff    ithcnt    tdcnof     ftall    unused    shltrw    igactc
         0         0         0         0         0                 0.0         0
*CONTROL_ENERGY
$     HGEN      RWEN    SLNTEN     RYLEN     IRGEN
$#    hgen      rwen    slnten     rylen     irgen     
         2         2         2         2         2
*CONTROL_TERMINATION
$   ENDTIM    ENDCYC     DTMIN    ENDENG    ENDMAS     NOSOL
$#  endtim    endcyc     dtmin    endeng    endmas     nosol     
{{endtim}}         0       0.0       0.01.000000E8         0
*CONTROL_TIMESTEP
$   DTINIT    TSSFAC      ISDO    TSLIMT     DT2MS      LCTM     ERODE     MS1ST
$#  dtinit    tssfac      isdo    tslimt     dt2ms      lctm     erode     ms1st
       0.0{{tssfac}}         0       0.0       0.0         0         0         0
$#  dt2msf   dt2mslc     imscl    unused    unused     rmscl    unused      ihdo
       0.0         0         0                           0.0                   0
*DATABASE_GLSTAT
$#      dt    binary      lcur     ioopt     
{{dt_001}}         0         0         1
*DATABASE_MATSUM
$#      dt    binary      lcur     ioopt     
{{dt_002}}         0         0         1
*DATABASE_TRHIST
$#      dt    binary      lcur     ioopt     
 {{dt_003}}        2         0         1
*DATABASE_BINARY_D3PLOT
$       DT      LCDT      BEAM     NPLTC    PSETID
$#      dt      lcdt      beam     npltc    psetid      
{{dt_004}}         0         0         0         0
*DATABASE_BINARY_D3THDT
$#      dt      lcdt      beam     npltc    psetid      
{{dt_005}}         0         0         0         0
*DATABASE_EXTENT_BINARY
$    NEIPH     NEIPS    MAXINT    STRFLG    SIGFLG    EPSFLG    RLTFLG    ENGFLG
$#   neiph     neips    maxint    strflg    sigflg    epsflg    rltflg    engflg
         6         0         3         0         1         1         1         1
$   CMPFLG    IEVERP    BEAMIP     DCOMP      SHGE     STSSZ    N3THDT   IALEMAT
$#  cmpflg    ieverp    beamip     dcomp      shge     stssz    n3thdt   ialemat
         0         0         0         1         1         1         2         1
$  NINTSLD   PKP_SEN      SCLP     HYDRO     MSSCL     THERM    INTOUT    NODOUT
$# nintsld   pkp_sen      sclp     hydro     msscl     therm    intout    nodout
         0         0       1.0         0         0         0                    
$     DTDT    RESPLT     NEIPB   QUADSLD    CUBSLD
$#    dtdt    resplt     neipb     quadr     cubic     
         1         0         0         0         0
*DATABASE_TRACER
$     TIME     TRACK         X         Y         Z    AMMGID
$#    time     track         x         y         z    ammgid       nid    radius
$# 需调整的量：xyz，观测点坐标输入
$# {{DATABASE_TRACER}}
       0.0         2    -149.6     -99.9      74.6         1         0       0.0
       0.0         2    -100.0     -99.9      74.6         1         0       0.0
       0.0         2     -75.0     -99.9      74.6         1         0       0.0
       0.0         2     -50.0     -99.9      74.6         1         0       0.0
       0.0         2       0.0     -99.9      74.6         1         0       0.0
       0.0         2    -149.6     -99.9      30.0         1         0       0.0
       0.0         2    -100.0     -99.9      30.0         1         0       0.0
       0.0         2     -75.0     -99.9      30.0         1         0       0.0
       0.0         2     -50.0     -99.9      30.0         1         0       0.0
       0.0         2       0.0     -99.9      30.0         1         0       0.0
       0.0         2    -149.6     -99.9       0.0         1         0       0.0
       0.0         2    -100.0     -99.9       0.0         1         0       0.0
       0.0         2     -75.0     -99.9       0.0         1         0       0.0
       0.0         2     -50.0     -99.9       0.0         1         0       0.0
       0.0         2       0.0     -99.9       0.0         1         0       0.0
       0.0         2    -149.6     -99.9     -30.0         1         0       0.0
       0.0         2    -100.0     -99.9     -30.0         1         0       0.0
       0.0         2     -75.0     -99.9     -30.0         1         0       0.0
       0.0         2     -50.0     -99.9     -30.0         1         0       0.0
       0.0         2       0.0     -99.9     -30.0         1         0       0.0
       0.0         2    -149.6     -99.9     -74.6         1         0       0.0
       0.0         2    -100.0     -99.9     -74.6         1         0       0.0
       0.0         2     -75.0     -99.9     -74.6         1         0       0.0
       0.0         2     -50.0     -99.9     -74.6         1         0       0.0
       0.0         2       0.0     -99.9     -74.6         1         0       0.0
       0.0         2    -149.6     -99.6      74.6         1         0       0.0
       0.0         2    -100.0     -99.6      74.6         1         0       0.0
       0.0         2     -75.0     -99.6      74.6         1         0       0.0
       0.0         2     -50.0     -99.6      74.6         1         0       0.0
       0.0         2       0.0     -99.6      74.6         1         0       0.0
       0.0         2    -149.6     -99.6      30.0         1         0       0.0
       0.0         2    -100.0     -99.6      30.0         1         0       0.0
       0.0         2     -75.0     -99.6      30.0         1         0       0.0
       0.0         2     -50.0     -99.6      30.0         1         0       0.0
       0.0         2       0.0     -99.6      30.0         1         0       0.0
       0.0         2    -149.6     -99.6       0.0         1         0       0.0
       0.0         2    -100.0     -99.6       0.0         1         0       0.0
       0.0         2     -75.0     -99.6       0.0         1         0       0.0
       0.0         2     -50.0     -99.6       0.0         1         0       0.0
       0.0         2       0.0     -99.6       0.0         1         0       0.0
       0.0         2    -149.6     -99.6     -30.0         1         0       0.0
       0.0         2    -100.0     -99.6     -30.0         1         0       0.0
       0.0         2     -75.0     -99.6     -30.0         1         0       0.0
       0.0         2     -50.0     -99.6     -30.0         1         0       0.0
       0.0         2       0.0     -99.6     -30.0         1         0       0.0 
       0.0         2    -149.6     -99.6     -74.6         1         0       0.0
       0.0         2    -100.0     -99.6     -74.6         1         0       0.0
       0.0         2     -75.0     -99.6     -74.6         1         0       0.0
       0.0         2     -50.0     -99.6     -74.6         1         0       0.0
       0.0         2       0.0     -99.6     -74.6         1         0       0.0
       0.0         2    -149.6     -80.0      74.6         1         0       0.0
       0.0         2    -100.0     -80.0      74.6         1         0       0.0
       0.0         2     -75.0     -80.0      74.6         1         0       0.0
       0.0         2     -50.0     -80.0      74.6         1         0       0.0
       0.0         2       0.0     -80.0      74.6         1         0       0.0
       0.0         2    -149.6     -80.0      30.0         1         0       0.0
       0.0         2    -100.0     -80.0      30.0         1         0       0.0
       0.0         2     -75.0     -80.0      30.0         1         0       0.0
       0.0         2     -50.0     -80.0      30.0         1         0       0.0
       0.0         2       0.0     -80.0      30.0         1         0       0.0
       0.0         2    -149.6     -80.0       0.0         1         0       0.0
       0.0         2    -100.0     -80.0       0.0         1         0       0.0
       0.0         2     -75.0     -80.0       0.0         1         0       0.0
       0.0         2     -50.0     -80.0       0.0         1         0       0.0
       0.0         2       0.0     -80.0       0.0         1         0       0.0
       0.0         2    -149.6     -80.0     -30.0         1         0       0.0
       0.0         2    -100.0     -80.0     -30.0         1         0       0.0
       0.0         2     -75.0     -80.0     -30.0         1         0       0.0
       0.0         2     -50.0     -80.0     -30.0         1         0       0.0
       0.0         2       0.0     -80.0     -30.0         1         0       0.0
       0.0         2    -149.6     -80.0     -74.6         1         0       0.0
       0.0         2    -100.0     -80.0     -74.6         1         0       0.0
       0.0         2     -75.0     -80.0     -74.6         1         0       0.0
       0.0         2     -50.0     -80.0     -74.6         1         0       0.0
       0.0         2       0.0     -80.0     -74.6         1         0       0.0
       0.0         2    -149.6     -50.0      74.6         1         0       0.0
       0.0         2    -100.0     -50.0      74.6         1         0       0.0
       0.0         2     -75.0     -50.0      74.6         1         0       0.0
       0.0         2     -50.0     -50.0      74.6         1         0       0.0
       0.0         2       0.0     -50.0      74.6         1         0       0.0
       0.0         2    -149.6     -50.0      30.0         1         0       0.0
       0.0         2    -100.0     -50.0      30.0         1         0       0.0
       0.0         2     -75.0     -50.0      30.0         1         0       0.0
       0.0         2     -50.0     -50.0      30.0         1         0       0.0
       0.0         2       0.0     -50.0      30.0         1         0       0.0
       0.0         2    -149.6     -50.0       0.0         1         0       0.0
       0.0         2    -100.0     -50.0       0.0         1         0       0.0
       0.0         2     -75.0     -50.0       0.0         1         0       0.0
       0.0         2     -50.0     -50.0       0.0         1         0       0.0
       0.0         2       0.0     -50.0       0.0         1         0       0.0
       0.0         2    -149.6     -50.0     -30.0         1         0       0.0
       0.0         2    -100.0     -50.0     -30.0         1         0       0.0
       0.0         2     -75.0     -50.0     -30.0         1         0       0.0
       0.0         2     -50.0     -50.0     -30.0         1         0       0.0
       0.0         2       0.0     -50.0     -30.0         1         0       0.0
       0.0         2    -149.6     -50.0     -74.6         1         0       0.0
       0.0         2    -100.0     -50.0     -74.6         1         0       0.0
       0.0         2     -75.0     -50.0     -74.6         1         0       0.0
       0.0         2     -50.0     -50.0     -74.6         1         0       0.0
       0.0         2       0.0     -50.0     -74.6         1         0       0.0
       0.0         2    -149.6       0.0     -74.6         1         0       0.0
       0.0         2    -100.0       0.0     -74.6         1         0       0.0
       0.0         2     -75.0       0.0     -74.6         1         0       0.0
       0.0         2     -50.0       0.0     -74.6         1         0       0.0
       0.0         2       0.0       0.0     -74.6         1         0       0.0
       0.0         2    -149.6       0.0     -30.0         1         0       0.0
       0.0         2    -149.6       0.0       0.0         1         0       0.0
       0.0         2    -149.6       0.0      30.0         1         0       0.0
       0.0         2    -149.6       0.0      74.6         1         0       0.0
*BOUNDARY_NON_REFLECTING
$#    ssid        ad        as  
         1       0.0       0.0
$#  抗爆结构pid为1    需调整：secid固定为15，mid：MaterialItemId,t1-t4都是抗爆结构壁厚单位cm，抗爆结构材料本构方程MAT_PLASTIC_KINEMATIC_TITLE动态生成  
*PART
$#                                                                         title
T15
$#     pid     secid       mid     eosid      hgid      grav    adpopt      tmid
         1        15{{mid_01}}         0         0         0         0         0                  
*SECTION_SHELL_TITLE
15mm
$HMSET
$HMNAME SETS      18Set_18
$      SID       DA1       DA2       DA3       DA4SOLVER
$#   secid    elform      shrf       nip     propt   qr/irid     icomp     setyp
        15         2       1.0         2       1.0         0         0         1
$#      t1        t2        t3        t4      nloc     marea      idof    edgset
{{Wall_t}}{{Wall_t}}{{Wall_t}}{{Wall_t}}       0.0       0.0       0.0         0
$#  箱体结构pid为2，需调整：secid固定为6，mid:MaterialItemId,t1-t4都是箱体结构壁厚单位cm，也是动态生成，目前没有材料
*PART
$HWCOLOR COMPS       2       4
$NAME
$#                                                                         title
T6
$      PID     SECID       MID     EOSID      HGID      GRAV    ADPOPT      TMID
$#     pid     secid       mid     eosid      hgid      grav    adpopt      tmid
         2         6{{mid_02}}         0         0         0         0         0
*SECTION_SHELL_TITLE
6mm
$HMSET
$HMNAME SETS      18Set_18
$      SID       DA1       DA2       DA3       DA4SOLVER
$#   secid    elform      shrf       nip     propt   qr/irid     icomp     setyp
         6         2       1.0         2       1.0         0         0         1
$#      t1        t2        t3        t4      nloc     marea      idof    edgset
{{Wall_t}}{{Wall_t}}{{Wall_t}}{{Wall_t}}       0.0       0.0       0.0         0
$#  炸药pid为10001 需调整：secid固定为1，mid，eosid都是MaterialItemId，炸药材料本构方程MAT_HIGH_EXPLOSIVE_BURN和状态方程EOS_JWL动态生成  
*PART
$NAME
$#                                                                         title
TNT
$      PID     SECID       MID     EOSID      HGID      GRAV    ADPOPT      TMID
$#     pid     secid       mid     eosid      hgid      grav    adpopt      tmid
     10001         1{{mid_03}}{{eosid3}}         1         0         0         0
*SECTION_SOLID_TITLE
eleur
$    SECID    ELFORM       AET
$#   secid    elform       aet   
         1        11         0
*HOURGLASS
$     HGID       IHQ        QM       IBQ        Q1        Q2        QB        QW
$#    hgid       ihq        qm       ibq        q1        q2    qb/vdc        qw
         1         0       0.1         0       1.5      0.06       0.1       0.1
$#  空气，固定不变，mid，eosid都是MaterialItemId
*PART
$NAME
$#                                                                         title
AIR
$      PID     SECID       MID     EOSID      HGID      GRAV    ADPOPT      TMID
$#     pid     secid       mid     eosid      hgid      grav    adpopt      tmid
     10002         1{{mid_04}}{{eosid4}}         1         0         0         0
*MAT_NULL_TITLE
AIR
$#     mid        ro        pc        mu     terod     cerod        ym        pr
         1  0.001293       0.0       0.0       0.0       0.0       0.0       0.0
*EOS_LINEAR_POLYNOMIAL_TITLE
AIR
$#   eosid        c0        c1        c2        c3        c4        c5        c6
         1-1.0000E-6       0.0       0.0       0.0       0.4       0.4       0.0
$#      e0        v0  
2.50000E-6       1.0
$#  除了空气材料是固定的，其它材料项动态生成  
{{MaterialItems}}
$#  需调整：z1和z2对应炸药z方向位置和长度；r1和r2对应在炸药半径，r1和r2都是charge_r，z1 -charge_l/2和z2是charge_l/2
*INITIAL_VOLUME_FRACTION_GEOMETRY
$    FMSID   FMIDTYP     BAMMG    NTRACE
$#   fmsid   fmidtyp     bammg    ntrace      
     10000         1         2         3
$# conttyp   fillopt     fammg        vx        vy        vz    radvel    unused
         4         0         1       0.0       0.0       0.0         0          
$#      x1        y1        z1        x2        y2        z2        r1        r2
       0.0       0.0{{z1_001}}       0.0       0.0{{z2_001}}{{r1_001}}{{r2_001}}
*INITIAL_DETONATION
$#     pid         x         y         z        lt         -    mmgset      
     10000       0.0       0.0       0.0       0.0         0         0
*CONSTRAINED_GLOBAL
$#      tc        rc       dir         x         y         z       tol   
         1         5         1       0.0       0.0       0.0       0.0
*CONSTRAINED_LAGRANGE_IN_SOLID
$     SSID      MSID     SSTYP     MSTYP     NQUAD     CTYPE     DIREC     MCOUP
$#   slave    master     sstyp     mstyp     nquad     ctype     direc     mcoup
         1     10000         0         1         0         4         3         0
$    START       END      PFAC      FRIC    FRCMIN      NORM   NORMTYP      DAMP
$#   start       end      pfac      fric    frcmin      norm   normtyp      damp
       0.01.00000E10       0.1       0.0       0.5         0         0       0.0
$       CQ      HMIN      HMAX     ILEAK     PLEAK   LCIDPOR     NVENT    IBLOCk
$#      cq      hmin      hmax     ileak     pleak   lcidpor     nvent  blockage
       0.0       0.0       0.0         2       0.1         0         0         0
*ALE_STRUCTURED_MESH
$#   mshid      dpid      nbid      ebid    unused    unused    unused    tdeath
         1     10000   4000000   4000000                              1.00000E16
$#   cpidx     cpidy     cpidz      nid0     lcsid     
         1         2         3   1000001   1000001
$#  需调整：n对应网格节点，x对应坐标---------------
*ALE_STRUCTURED_MESH_CONTROL_POINTS
$#    cpid    unused    unused       sfo    unused      offo    
         1                           1.0                 0.0
$#                 n                   x               ratio     
                   1          {{ale_01}}                 0.0
                  31          {{ale_02}}                 0.0
          {{ale_03}}                 0.0                 0.0
*ALE_STRUCTURED_MESH_CONTROL_POINTS
$#    cpid    unused    unused       sfo    unused      offo    
         2                           1.0                 0.0
$#                 n                   x               ratio     
                   1          {{ale_04}}                 0.0
                  51          {{ale_05}}                 0.0
          {{ale_06}}          {{ale_07}}                 0.0
          {{ale_08}}          {{ale_09}}                 0.0
*ALE_STRUCTURED_MESH_CONTROL_POINTS
$#    cpid    unused    unused       sfo    unused      offo    
         3                           1.0                 0.0
$#                 n                   x               ratio     
                   1          {{ale_10}}                 0.0
                  51          {{ale_11}}                 0.0
          {{ale_12}}          {{ale_13}}                 0.0
          {{ale_14}}          {{ale_15}}                 0.0
$$
$$ Sets Defined In HyperMesh
$$
*ALE_MULTI-MATERIAL_GROUP
$      SID    IDTYPE
$#     sid    idtype    gpname      
     10001         1          
$      SID    IDTYPE
     10002         1          
*SET_PART_LIST
$HMSET
$HMNAME SETS      18Set_18
$      SID       DA1       DA2       DA3       DA4SOLVER
$#     sid       da1       da2       da3       da4    solver      
         1       0.0       0.0       0.0       0.0MECH
$#    pid1      pid2      pid3      pid4      pid5      pid6      pid7      pid8
         1         2         0         0         0         0         0         0
*SET_SEGMENT_GENERAL
$      SID       DA1       DA2       DA3       DA4SOLVER
$#     sid       da1       da2       da3       da4    solver      
         1       0.0       0.0       0.0       0.0MECH
$#  option     mshid        -x        +x        -y        +y        -z        +z
SALEFAC            1         1         0         1         1         1         1
*DEFINE_COORDINATE_NODES
$#     cid        n1        n2        n3      flag       dir   
   1000001   1000002   1000003   1000004         0X
*INCLUDE
model.k
*END
