IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'OptoAssist')
    DROP DATABASE OptoAssist;
-- Create database
CREATE DATABASE OptoAssist;
GO

-- Use the database
USE OptoAssist;
GO

/***************************************************************/
/***                     Creating tables                     ***/
/***************************************************************/

/* Table: dbo.Staff */
CREATE TABLE QuestionTable
(
    topic NVARCHAR(255), -- Assuming a maximum length of 255 characters for the topic
    question NVARCHAR(MAX) -- Assuming a large text field for the question
);

-- Insert data into the QuestionTable
INSERT INTO QuestionTable (topic, question)
VALUES
    ('chief complaint', '["What brings you in for an eye examination today?", "Can you describe the main issue or concern you have with your eyes?"]'),
    ('medical history', '["Do you have any existing medical conditions?", "Are you currently taking any medications or eye drops?", "Do you have any known allergies to medications or substances?", "Have you ever had eye surgery or any eye-related procedures in the past?"]'),
    ('symptoms', '["Could you describe any symptoms you''re experiencing, such as pain, redness, or discomfort?", "When did these symptoms start?", "Are the symptoms constant or intermittent?", "Have you noticed any triggers or patterns related to the symptoms?", "Do you have any associated symptoms?"]'),
    ('pain', '["Which eye had the pain?", "What is the severity of the pain between 1 to 10, 10 being the most painful?", "How long have you had the pain?", "How would you describe the pain?", "Is there any other issues that you think are related to the pain? e.g. redness?", "Have you had this kind of pain before?", "Each time you had the pain, how long did it last?"]'),
    ('history taking', '["Can you describe the patient''s chief complaint or reason for the visit?", "Ask about the onset and duration of symptoms.", "Inquire about any relevant medical history or pre-existing conditions.", "Ask about the family history of eye diseases.", "Explore lifestyle factors like smoking, allergies, and occupation.", "Encourage the patient to share any other symptoms or concerns."]'),
	('red eyes','["When did you first notice that your eyes were red, and has the redness been persistent?", "Do you experience any pain or discomfort in addition to the redness?", "Have you used any over-the-counter eye drops or home remedies to address the redness?", "Do you wear contact lenses, and if so, have you noticed any changes in redness since using them?", "Have you been exposed to any potential irritants or had any recent eye injuries?"]'),
	('pain','["Which eye is affected by the pain you''re experiencing?", "On a scale from 1 to 10, with 10 being the most severe, how would you rate the intensity of the pain?", "How long have you been dealing with this pain?", "Can you describe the nature of the pain, such as sharp, dull, throbbing, or burning?", "Have you noticed any other issues, like redness, associated with the pain?", "Is this the first time you''ve experienced this type of pain, or has it occurred before?", "During each occurrence of the pain, how long does it typically last?"]'),
	('blurred vision','["When did you first notice changes in your vision, such as blurriness?", "Does the blurred vision occur in one or both eyes?", "Is the blurriness constant, or does it come and go?", "Have you experienced any other visual disturbances, such as flashes of light or floaters?", "Do you wear corrective lenses, and if so, are they up-to-date?"]'),
	 ('tearing or watery eyes','["When did you first notice excessive tearing or watery eyes?", "Is the tearing accompanied by any other symptoms, such as itching or redness?", "Have you been exposed to any environmental factors that might be contributing to the tearing?", "Do you wear contact lenses, and if so, have you noticed any changes since the tearing started?"]');

select * from dbo.QuestionTable
