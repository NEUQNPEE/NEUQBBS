�
.E:\CX\WebapiDemo\WebApiDemoCommon\Md5Helper.cs
	namespace 	
WebApiDemoCommon
 
{		 
public

 

static

 
class

 
	Md5Helper

 !
{ 
public 
static 
string 
ToMd5 "
(" #
this# '
string( .
str/ 2
)2 3
{
byte 
[ 
] 
output 
= 
MD5 
.  
HashData  (
(( )
Encoding) 1
.1 2
Default2 9
.9 :
GetBytes: B
(B C
strC F
)F G
)G H
;H I
var 
md5Str 
= 
BitConverter %
.% &
ToString& .
(. /
output/ 5
)5 6
.6 7
Replace7 >
(> ?
$str? B
,B C
$strD F
)F G
;G H
return 
md5Str 
; 
} 	
} 
} 