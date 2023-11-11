--create database OptoAssist
--go

use OptoAssist

/***************************************************************/
/***                     Creating tables                     ***/
/***************************************************************/

/* Table: dbo.Staff */
--CREATE TABLE QuestionTable
--(
--    topic NVARCHAR(255), -- Assuming a maximum length of 255 characters for the topic
--    question NVARCHAR(MAX) -- Assuming a large text field for the question
--);

-- Drop the QuestionTable
--DROP TABLE QuestionTable;

--#1
--INSERT INTO QuestionTable (topic, question)
--VALUES
--    ('chief complaint', '["What brings you in for an eye examination today?", "Can you describe the main issue or concern you have with your eyes?"]'),
--    ('medical history', '["Do you have any existing medical conditions?", "Are you currently taking any medications or eye drops?", "Do you have any known allergies to medications or substances?", "Have you ever had eye surgery or any eye-related procedures in the past?"]'),
--    ('symptoms', '["Could you describe any symptoms you''re experiencing, such as pain, redness, or discomfort?", "When did these symptoms start?", "Are the symptoms constant or intermittent?", "Have you noticed any triggers or patterns related to the symptoms?", "Do you have any associated symptoms?"]'),
--    ('pain', '["Which eye had the pain?", "What is the severity of the pain between 1 to 10, 10 being the most painful?", "How long have you had the pain?", "How would you describe the pain?", "Is there any other issues that you think are related to the pain? e.g. redness?", "Have you had this kind of pain before?", "Each time you had the pain, how long did it last?"]'),
--    ('history taking', '["Can you describe the patient''s chief complaint or reason for the visit?", "Ask about the onset and duration of symptoms.", "Inquire about any relevant medical history or pre-existing conditions.", "Ask about the family history of eye diseases.", "Explore lifestyle factors like smoking, allergies, and occupation.", "Encourage the patient to share any other symptoms or concerns."]');

--#2
--INSERT INTO QuestionTable (topic, question)
--VALUES ('chief complaint','"What brings you in for an eye examination today?"'),('chief complaint','"Can you describe the main issue or concern you have with your eyes?"')
--		,('medical history','"Do you have any existing medical conditions?"'),('medical history','"Are you currently taking any medications or eye drops?"'),('medical history','"Do you have any known allergies to medications or substances?"'),('medical history','"Have you ever had eye surgery or any eye-related procedures in the past?"')
--		,('symptoms','"Could you describe any symptoms you''re experiencing, such as pain, redness, or discomfort?"'),('symptoms','"When did these symptoms start?"'),('symptoms','"Are the symptoms constant or intermittent?"'),('symptoms','"Have you noticed any triggers or patterns related to the symptoms?"'),('symptoms','"Do you have any associated symptoms?"')
--		,('pain','"Which eye had the pain?"'),('pain','"What is the severity of the pain between 1 to 10, 10 being the most painful?"'),('pain','"How long have you had the pain?"'),('pain','"How would you describe the pain?"'),('pain','"Is there any other issues that you think are related to the pain? e.g. redness?"'),('pain','"Have you had this kind of pain before?"'),('pain',' "Each time you had the pain, how long did it last?"')
--		,('history taking','"Can you describe the patient''s chief complaint or reason for the visit?"'),('history taking','"Ask about the onset and duration of symptoms."'),('history taking','"Inquire about any relevant medical history or pre-existing conditions."'),('history taking','"Ask about the family history of eye diseases."'),('history taking','"Explore lifestyle factors like smoking, allergies, and occupation."'),('history taking','"Encourage the patient to share any other symptoms or concerns."')

select * from dbo.QuestionTable
