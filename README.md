# DollMakerProject

Canvas link [Present Slide](https://www.canva.com/design/DAFtArTWi1c/c9ytBSd-RjjXn-7kVb6Drg/edit).
ตารางงาน [Plan Work](https://docs.google.com/spreadsheets/d/1hgN81cO__76_1bTk-sFhRkZlBrURaWD6x2bH2btdZRY/edit#gid=0).
# Game Design Doc 
## Introduction 


### -Game Concept 
 2.5D Action horror สร้างตุ๊กตาส่งลูกค้าแล้วก็ต้องมาค่อยไล่ผีอีก! 
### -มีจุดเด่นและแตกต่างกับเกมอื่นในตลาด 
 การสร้างตุ๊กตาในเกมแนว Action 
### -หมวดหมู่ของเกม 
 2.5D Comedy Horror 
### -เนื้อเรืองโดยสังเขปของเกม 
 นักทำตุ๊กตาคุนไสยที่ได้รับออเดอร์จากหมอปลาให้ทำตุ๊กตาให้ 69 แต่บ้านดันมาสร้างทับที่สุสานเก่าเลยมีวิญญาณจ้องจะมาปล้นตุ๊กตาไปก่ออาชญากรรมแบบตุ๊กตาแช็คกี้ 
### -บรรยายกาศของเกม 
 หนาวๆเย็นๆ มืดๆมีแค่แสงจากตะเกียงที่ให้ความรู้สึกอบอุ่น 
### -กลุ่มเป้าหมายผู้เล่น 
 สร้างตุ๊กตาให้หมอปลาให้ครบ 66 ตัวภายใน 6 วัน 

 
## Game Structure 

### -โหมดเกมที่ผู้เล่นสามารถเล่นได้ 
 โหมดเนื้อเรื่องอย่างเดียว
    
### -Engagement  
 การที่เราสามารถทำร้ายผีได้ด้วยวิธีการที่ตรงไปตรงมาง่ายๆ เช่นการโยนไม้กางเขนใส่หน้ามันราวกับว่ามันไม่ใช่ผี ให้ความรู้สึกของคนที่อาจจะไม่ชอบที่วิญญาณมันมีพลังอะไรขนาดนั้น ทำไมเราถึงสู้กลับพวกผีไม่ได้เลย เกมนี้จะให้เราได้บูลลี่ผีได้แบบซะใจ

### -การเล่น 
 -Gameplayแบ่งออกเป็น 2 ส่วน
	1.การสร้างตุ๊กตา - เป็นกันกดทำ Action ที่โต๊ะทำตุ๊กตาด้วยการเย็บผ้าเป็น Gameplay แบบ skill check 
		เงื่อนไขในการที่จะใช้เครื่องจักรทำตุ๊กตาได้
           	1.ต้องมีชิ้นส่วนตุ๊กตาครบ 5 ชิ้น
           	2.ต้องมีชิ้นส่วนผ้าให้ครบ 3 ชิ้น                 
		*หากมีไม่ครบจะไม่สามารถกดใช้เครื่องทำตุ๊กตาได้*     
	2.ไล่ผี - ผู้เล่นจะมีไอเทมที่ใช้ไล่ผี คือ ไม้กางเขน ผีจะตรวจจับการมองเห็นและการได้ยินเสียงของผู้เล่นหรือการกระทำต่างๆของผู้เล่นแล้วจะเดินไปมาหาเพื่อมาทำร้ายผู้เล่น 
	โดยที่ผู้เล่นจะต้องล่อหรือใช้ไม้กางเขน ทุบผีให้ผีติดสตั้นและหายไปเกิดใหม่ในจุดอื่นในบ้านแทน ซึ่งประตูในห้องทำตุ๊กตาของผู้เล่นจะสามารถบล็อคผีได้แต่ผีก็สามารถทำลายได้โดยมีขีดจำกัดเป็นเหมือนเลือดของ 
 	ประตู 3 ขีด ผีจะต้องตีประตูขีดละ 3 ครั้ง ถึงจะลด 1 ขีด เมื่อประตูแตกจะทำให้ความปลอดภัยของห้องน้อยลง

  Event - เป็น Random Event ที่จะเกิดขึ้นในขณะที่ผู้เล่นกำลังนั่งทำตุ๊กตา ผู้เล่นจะโดนผีอำทำให้มองไม่เห็นของที่ต้องใช้ในการทำตุ๊กตาและผีจะนำไปซ่อนในบ้านจุดต่างๆแบบ Random

 
## Graphics & Sound 

### -มุมมองของเกม 
 เป็นมุมมองบุคคลที่ 1 (First Person)
   
### -สไตล์กราฟฟิก  
 ภาพจะมีความ Cartoon แต่ไปในโทนหม่นๆหมองๆไม่สดใสมากนัก เช่นเกม Spooky jumpscare mansion
 
### -Animation  
 จะมี Cutscene เล่าเรื่องช่วงต้นที่เราจะคุยกับหมอผีเพื่อดีลทำตุ๊กตาให้ Action ของตัวผู้เล่นจะเป็น Animation 2D
 
### -เสียง Effect Enviroment & Music 
 ให้ความรู้สึกที่เย็นๆหนาวๆ บรรยากาศมีลมพัดเข้ามาในบ้าน
 
  
## Game Play  

### -Character  
 โกโจ้ จามจุรี - ผู้เล่นจะสามารถถือ Item ได้ 1 อย่างโดยตัวเราจะมีตะเกียงไฟติดตัวเอาไว้ให้แสงสว่าง และไอเทมที่เลือกเก็บเช่น ไม้กางเขน หรือชิ้นส่วนตุ๊กตาหรือผ้าที่ใช้เย็บตุ๊กตา
 นิสัย- เป็นคนที่หลงไหลในการทำตุ๊กตามาก ในช่วงแรกเขาค่อนข้างกลัวผีเพราะไม่รู้วิธีรับมือ แต่ก็ได้คำแนะนำจากพวกลูกค้าที่เป็นหมอผี 
 เรื่องของที่ผีไม่ชอบและกลัว แต่ด้วยเขาเป็นคนไม่ชอบทำไรให้มันยุ่งยากเขาจึงใช้แค่ ไม้กางเขนแค่อันเดียวของเขาเข้าไปฟาดผี หรือชูใส่หน้าพวกมัน ก็สามารถจัดการได้แล้ว 
 เขาเป็นคนที่เวลาทำงานต้องการสมาธิมาก พอมีอะไรมากวนเขาก็จะทำให้เขาหงุดหงิดและไม่เป็นอันทำงานต่อ

### -Story  
 คุณเป็นคนทำตุ๊กตาคุนไสยเพื่อเอาไปขายให้กับหมอผีหรือผู้ที่ต้องทำพิธีศักสิท ในช่วงเวลานี้ของทุกปีจะมีผู้ใช้คุนไสยคนหนึ่งมาจ้างให้คุณทำตุ๊กตาให้เขาจำนวน 66 ตัว ถายใน 6 วัน บ้านที่คุณอยู่เคยเป็นสุสานเก่า จึงมีวิญญาณมากมายที่เคยอาศัยอยู่จ้องจะเข้ามาสิงในตุ๊กตาที่คุณทำ คุณเลยต้องคอยปัดเป่าและกำจัดวิญญาณร้ายพวกนี้เพื่อไม่ให้พวกมันมาสิงและขโมยตุ๊กตาของคุณไป หรือเอาไปใช้ประโยชน์ในทางที่ไม่ดี คุณอยู่อาศัยในบ้านหลังนี้มานานพบเจอวิญญาณมากมายแต่มีแค่ช่วงนี้ที่วิญญาณมาแวะเวียนเยอะเพราะ ได้ลูกค้ารายใหญ่สั่งทำตุ๊กตาหลายตัว พวกวิญญาณที่คิดร้ายเลยใช้จังหวะนี้ในการแอบเข้ามาขโมยหรือสิงเข้าไปในตุ๊กตาเพื่อที่จะไปทำเรื่องไม่ดีกับผู้อื่น ตัวเราจึงต้องไล่ผีพวกนี้ไปก่อนเพื่อไม่ให้มันมาขโมยตุ๊กตาของเราได้และเราจะได้ทำตุ๊กตาให้เสร็จทันวันที่จะต้องส่งให้ลูกค้า
 
### -Objective  
 คุณจะต้องทำตุ๊กตาให้ครบทั้ง 66 ตัว ภายใน 6 วัน ในแต่ละวันจะต้องทำให้ได้ 11 ตัว
  
### -Core Game Mechanic  
 การสร้างและประกอบตุ๊กตา การทำตุ๊กตาจะทำให้เกิดเสียง และเราจะต้องคอยระวังไม่ให้ผีมาทำร้ายเรา เพราะไม่งั้นคุณอาจจะตายได้

### -Level Design  
 จะมีทั้งหมด 6 รอบที่ผู้เล่นจะต้องเล่นและในแต่ละวันจะมีกำหนดจำนวนตุ๊กตาที่ต้องทำให้ได้อยู่ของวันนั้นๆ
 
### -Progression  
 ในแต่ละวันจะมีระดับความถี่และจำนวนวิญญาณที่มาก่อกวนมากขึ้น และจะสอนให้ผู้เล่นรู้จักของใช้กันวิญญาณใหม่ๆในแต่ละวันเพื่อกันผีในแต่ละแบบ

### -Enemy หรือ NPC มีกี่แบบ  
 Enemy - วิญญาณที่จะเข้ามาในห้องของเราเพื่อขโมยตุ๊กตาของเราที่ทำเสร็จแล้ว และหากพวกมันขโมยไปมากพอจะทำให้เกม Over ได้
 NPC - จะมีวิญญาณบางตัวที่ยืนเฉยๆให้เราเข้าไปคุยได้โดยที่มันอาจจะช่วยบอกจุดที่ซ่อนของที่ขโมยโดยวิญญาณตัวอื่นไป หรืออาจจะไม่ช่วยอะไรเราเลยกวนเราเฉยๆ 
 	
 
 

