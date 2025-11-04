*KEYWORD
*CONTROL_BULK_VISCOSITY
$#      q1        q2      type     btype    tstype
       1.5      0.06         1         0         0
*CONTROL_CONTACT
$#  slsfac    rwpnal    islchk    shlthk    penopt    thkchg     orien    enmass
{{slsfac}}       0.0         1         0         1         0         1         0
$#  usrstr    usrfrc     nsbcs    interm     xpene     ssthk      ecdt   tiedprj
         0         0         0         0       4.0         0         0         0
$#   sfric     dfric       edc       vfc        th     th_sf    pen_sf     ptscl
       0.0       0.0       0.0       0.0       0.0       0.0       0.0       1.0
$#  ignore    frceng   skiprwg    outseg   spotstp   spotdel   spothin   dir_tie
         0         0         0         0         0         0       0.0         0
$#    isym    nserod    rwgaps    rwgdth     rwksf      icov    swradf    ithoff
         0         0         1       0.0       1.0         0       0.0         0
$#  shledg    pstiff    ithcnt    tdcnof     ftall    unused    shltrw    igactc
         0         0         0         0         0                 0.0         0
*CONTROL_ENERGY
$#    hgen      rwen    slnten     rylen     irgen     maten     drlen     disen
         2         2         2         2         2         1         1         1
*CONTROL_HOURGLASS
$#     ihq        qh
         4       0.1
*CONTROL_TERMINATION
$#  endtim    endcyc     dtmin    endeng    endmas     nosol
{{endtim}}         0       0.0       0.01.000000E8         0
*CONTROL_TIMESTEP
$#  dtinit    tssfac      isdo    tslimt     dt2ms      lctm     erode     ms1st
       0.0{{tssfac}}         0       0.0       0.0         0         0         0
$#  dt2msf   dt2mslc     imscl    unused    unused     rmscl     emscl      ihdo
       0.0         0         0                           0.0       0.0         0
*DATABASE_GLSTAT
$#      dt    binary      lcur     ioopt
{{dt_001}}         0         0         1
*DATABASE_GLSTAT_MASS_PROPERTIES
$#      dt    binary      lcur     ioopt
{{dt_002}}         0         0         1
*DATABASE_MATSUM
$#      dt    binary      lcur     ioopt
{{dt_003}}         0         0         1
*DATABASE_BINARY_D3PLOT
$#      dt      lcdt      beam     npltc    psetid
{{dt_004}}         0         0         0         0
$#   ioopt      rate    cutoff    window      type      pset
         0       0.0       0.0       0.0         0         0
*DATABASE_BINARY_D3THDT
$#      dt      lcdt      beam     npltc    psetid
{{dt_005}}         0         0         0         0
*DATABASE_EXTENT_BINARY
$#   neiph     neips    maxint    strflg    sigflg    epsflg    rltflg    engflg
         6         4         3         0         1         1         1         1
$#  cmpflg    ieverp    beamip     dcomp      shge     stssz    n3thdt   ialemat
         0         0         0         1         1         1         2         1
$# nintsld   pkp_sen      sclp     hydro     msscl     therm    intout    nodout
         0         0       1.0         0         0         1                    
$#    dtdt    resplt     neipb   quadsld    cubsld   deleres
         0         0         0         0         0         0
$#
*DATABASE_FORMAT
$#   iform   ibinary
         0         1
*CONTACT_AUTOMATIC_GENERAL_ID
$#     cid                                                                 title
       101                                                                      
$#   surfa     surfb  surfatyp  surfbtyp   saboxid   sbboxid      sapr      sbpr
         3         0         2         0         0         0         0         0
$#      fs        fd        dc        vc       vdc    penchk        bt        dt
       0.0       0.0       0.0       0.0       0.0         0       0.01.00000E20
$#    sfsa      sfsb      sast      sbst     sfsat     sfsbt       fsf       vsf
       3.0       1.0       0.0       0.0       1.0       1.0       1.0       1.0
$# 将上复合板、蜂窝和下复合板分别设置为一个整体
$# SET_PART_LIST 出现3次，sid 依次为1、2、3和对应的pid，接触之前
$# sid = 1 只有弹体，PID 分别为81、82、83
$# sid = 2 复合板和蜂窝 PID 分别为 1001~1000+n+m+1
$# sid = 3 弹体、复合板和蜂窝	81、82、83, 1001~1000+n+m+1
{{SET_PART_LIST}}
$# 弹体和靶板接触-固定的不变
*CONTACT_ERODING_SURFACE_TO_SURFACE_ID
$#     cid                                                                 title
        61                                                                      
$#   surfa     surfb  surfatyp  surfbtyp   saboxid   sbboxid      sapr      sbpr
         2         1         2         2         0         0         0         0
$#      fs        fd        dc        vc       vdc    penchk        bt        dt
       0.0       0.0       0.0       0.0       0.0         2       0.01.00000E20
$#    sfsa      sfsb      sast      sbst     sfsat     sfsbt       fsf       vsf
       1.0       1.0       0.0       0.0       1.0       1.0       1.0       1.0
$#    isym    erosop      iadj
         1         1         0
$#    soft    sofscl    lcidab    maxpar     sbopt     depth     bsort    frcfrq
         2       0.1         0     1.025         2         2         0         1
$#  penmax    thkopt    shlthk     snlog      isym     i2d3d    sldthk    sldstf
       0.0         0         0         0         0         0       0.0       0.0
$# 弹体之间接触-固定的不变
*CONTACT_ERODING_SURFACE_TO_SURFACE_ID
$#     cid                                                                 title
        61                                                                      
$#   surfa     surfb  surfatyp  surfbtyp   saboxid   sbboxid      sapr      sbpr
        82        81         3         3         0         0         0         0
$#      fs        fd        dc        vc       vdc    penchk        bt        dt
       0.0       0.0       0.0       0.0       0.0         2       0.01.00000E20
$#    sfsa      sfsb      sast      sbst     sfsat     sfsbt       fsf       vsf
       1.0       1.0       0.0       0.0       1.0       1.0       1.0       1.0
$#    isym    erosop      iadj
         1         1         0
$#    soft    sofscl    lcidab    maxpar     sbopt     depth     bsort    frcfrq
         2       0.1         0     1.025         2         2         0         1
$#  penmax    thkopt    shlthk     snlog      isym     i2d3d    sldthk    sldstf
       0.0         0         0         0         0         0       0.0       0.0
$# 弹体之间接触2-固定的不变
*CONTACT_ERODING_SURFACE_TO_SURFACE_ID
$#     cid                                                                 title
        61                                                                      
$#   surfa     surfb  surfatyp  surfbtyp   saboxid   sbboxid      sapr      sbpr
        82        83         3         3         0         0         0         0
$#      fs        fd        dc        vc       vdc    penchk        bt        dt
       0.0       0.0       0.0       0.0       0.0         2       0.01.00000E20
$#    sfsa      sfsb      sast      sbst     sfsat     sfsbt       fsf       vsf
       1.0       1.0       0.0       0.0       1.0       1.0       1.0       1.0
$#    isym    erosop      iadj
         1         1         0
$#    soft    sofscl    lcidab    maxpar     sbopt     depth     bsort    frcfrq
         2       0.1         0     1.025         2         2         0         1
$#  penmax    thkopt    shlthk     snlog      isym     i2d3d    sldthk    sldstf
       0.0         0         0         0         0         0       0.0       0.0
$# 弹体之间接触3-固定的不变
*CONTACT_ERODING_SURFACE_TO_SURFACE_ID
$#     cid                                                                 title
        61                                                                      
$#   surfa     surfb  surfatyp  surfbtyp   saboxid   sbboxid      sapr      sbpr
        83        81         3         3         0         0         0         0
$#      fs        fd        dc        vc       vdc    penchk        bt        dt
       0.0       0.0       0.0       0.0       0.0         2       0.01.00000E20
$#    sfsa      sfsb      sast      sbst     sfsat     sfsbt       fsf       vsf
       1.0       1.0       0.0       0.0       1.0       1.0       1.0       1.0
$#    isym    erosop      iadj
         1         1         0
$#    soft    sofscl    lcidab    maxpar     sbopt     depth     bsort    frcfrq
         2       0.1         0     1.025         2         2         0         1
$#  penmax    thkopt    shlthk     snlog      isym     i2d3d    sldthk    sldstf
       0.0         0         0         0         0         0       0.0       0.0
$# 设置复合板相邻板之间的接触  for循环
{{BoardContacts}}
$# MAT导入的材料-当前项目所拥有的MaterialItem
{{MaterialItems}}
$# 只定义一次-固定的
*SECTION_SOLID_TITLE
SOLID
$#   secid    elform       aet    unused    unused    unused    cohoff   gaskeit
         1         1         0                                     0.0       0.0
$#  遍历 ：81、82、83, 1001~1000+n+m+1 个PART 
$#  PID：就是item
$#  secid：1
$#  mid：MaterialItemId
$#  eosid：0/MaterialItemId
{{Parts}}
$# 对称边界
*CONSTRAINED_GLOBAL
$#      tc        rc       dir         x         y         z       tol
         1         5         1       0.0       0.0       0.0       0.0
         2         6         2       0.0       0.0       0.0       0.0
$# 设置弹体速度 - 速度除以10000，只变更VZ，加上负号例如：vz -0.077
*INITIAL_VELOCITY_GENERATION
$#      id      styp     omega        vx        vy        vz     ivatn      icid
$#       1         1       0.0       0.0       0.0    -0.077         0         0
         1         1       0.0       0.0       0.0{{vz_001}}         0         0
$#      xc        yc        zc        nx        ny        nz     phase    irigid
       0.0       0.0       0.0       0.0       0.0       0.0         0         0
$# 导入模型文件和弹体文件
*INCLUDE_transform
model.k
1000000,1000000



*INCLUDE
bullet.k
$# 结束
*END