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
    ('history taking', '["Get reason for visit.", " Get personal and demographic data.", " Get history of present complain.", " Get past ocular history.", "Get family ocular history.", "Encourage the patient to share any other symptoms or concerns."]'),
	('red eyes','["When did you first notice that your eyes were red, and has the redness been persistent?", "Do you experience any pain or discomfort in addition to the redness?", "Have you used any over-the-counter eye drops or home remedies to address the redness?", "Do you wear contact lenses, and if so, have you noticed any changes in redness since using them?", "Have you been exposed to any potential irritants or had any recent eye injuries?"]'),
	('pain','["Which eye is affected by the pain you''re experiencing?", "On a scale from 1 to 10, with 10 being the most severe, how would you rate the intensity of the pain?", "How long have you been dealing with this pain?", "Can you describe the nature of the pain, such as sharp, dull, throbbing, or burning?", "Have you noticed any other issues, like redness, associated with the pain?", "Is this the first time you''ve experienced this type of pain, or has it occurred before?", "How long does an occurence of this pain typically last?"]'),
	('blurred vision','["When did you first notice changes in your vision, such as blurriness?", "Does the blurred vision occur in one or both eyes?", "Is the blurriness constant, or does it come and go?", "Have you experienced any other visual disturbances, such as flashes of light or floaters?", "Do you wear corrective lenses, and if so, are they up-to-date?"]'),
	('tearing','["When do you notice your eyes tearing?","Is there a specific time of day or situation?","Are there any activities or environmental factors that seem to trigger tearing?"]'),
	('watery eyes','["When did you first notice excessive tearing or watery eyes?", "Is the tearing accompanied by any other symptoms, such as itching or redness?", "Have you been exposed to any environmental factors that might be contributing to the tearing?", "Do you wear contact lenses, and if so, have you noticed any changes since the tearing started?"]'),
	('myopia','["When were you first diagnosed with nearsightedness?","Have you noticed any changes in your nearsightedness recently?"]'),
	('allergies','["Are you aware of any allergies, especially those affecting your eyes?","Do you notice a pattern of symptoms related to specific allergens?"]');

	 CREATE TABLE QuestionTableChild
(
    topic NVARCHAR(255), -- Assuming a maximum length of 255 characters for the topic
    question NVARCHAR(MAX) -- Assuming a large text field for the question
);

INSERT INTO QuestionTableChild(topic, question)
VALUES
('chief Complaint','["When did you first notice any problems with your eyes or vision?","Can you tell me what happened right before your eyes started feeling different?","Can you show me or describe where it hurts or feels uncomfortable around your eyes or head?","How long does the discomfort or vision issue last each time?","Is it constant, or does it come and go?","Can you tell me more about what you''re feeling?","For example, is it like a burning sensation, itching, or maybe like something is in your eye?"]'),
('history taking', '["get reason for visit", " get personal and demographic data", " get history of present complain", " get Past ocular history", "get family ocular history", "Encourage the patient to share any other symptoms or concerns."]'),
('medical History','["Have you noticed anything that makes it feel better?","Does resting your eyes, closing them, or using any specific measures provide relief?","Are there any specific actions or treatments that seem to make the discomfort or vision issues go away temporarily?"]'),
('symptoms','["Does the discomfort or vision problem happen at a particular time of day or during specific activities, like reading or playing video games?","On a scale from 1 to 10, with 1 being no discomfort at all and 10 being the most severe discomfort, how would you rate the intensity of your eye or vision issues?"]'),
('pain','["Can you remember what you were doing when the pain or discomfort in your eyes started?","Where exactly do you feel the pain?","Can you point to the spot?","How long does the pain last each time?","Can you describe the pain?","Is it sharp, dull, or throbbing?"]'),
('red eyes','["When did you first notice that your eyes were red?","Do both of your eyes look red, or is it just one?","How long do your eyes stay red each time?","Can you describe any other symptoms associated with the redness, like itching or burning?"]'),
('Blurred Vision','["When did you first notice that your vision was blurry?","Is the blurriness in one eye or both?","How long does your vision stay blurry each time?","Can you describe what the blurriness looks like?","Is it like looking through fog or something else?"]'),
('Tearing','["When did you first notice that your eyes started tearing or watering?","Do both eyes tear, or is it just one?","How long do your eyes tear or water each time?","Can you tell me if your eyes feel itchy or irritated when they tear?"]'),
('Watery eye','["Have you noticed specific things that make your eyes water more, like being in the wind, being around pets, or when you''re outside playing?","Do you ever have trouble seeing the board or reading at school when your eyes are watery?","Are there any home remedies or specific things you do that seem to help when your eyes are excessively watery?"]'),
('myopia','["When did you first notice that things far away were blurry?","Was there a specific time or event?","Can you show me or describe where you notice the blurriness?","Is it more in one eye than the other?","How long does your vision stay blurry when you''re trying to see things far away?","Can you explain what it''s like when things are blurry?","Is it like seeing things through a fog, or is it different?"]'),
('allergies','["When did you first notice any problems with your eyes related to allergies?","Was there a specific time or situation?","Can you show me or describe where you feel the discomfort or irritation in your eyes when allergies act up?","How long do the symptoms last each time you experience an allergic reaction?","Is it a constant issue or does it come and go?","Is it itching, burning, or something else?"]');

CREATE TABLE QuizQn (
    qnID INT PRIMARY KEY,
    qnText VARCHAR(1000),
    qnImage VARCHAR(255), -- Stores path of image to be used
    qnCat VARCHAR(100),
	qnAns VARCHAR(1000)
);

INSERT INTO QuizQn (qnID, qnText, qnImage, qnCat, qnAns) VALUES
(1, 'A patient complains of redness and irritation in their left eye. What follow-up question would you ask?', NULL, 'Eye Conditions', 'duration,how long,pain,severity,vision'),
(2, 'A patient reports a family history of glaucoma. How would you further inquire about this?', NULL, 'Medical History', 'details,elaborate,family history,age'),
(3, 'A patient experiences sudden vision loss in one eye. What follow-up question would you pose?', NULL, 'Eye Conditions', 'recurring,duration,timing,how long,other symptoms,accompanying symptoms'),
(4, 'A patient complains of seeing floating specks or cobwebs in their vision. What follow-up question would you ask?', NULL, 'Eye Conditions', 'onset,duration,how long,accompanying symptoms,appearance,movement,fixed'),
(5, 'A patient with a history of diabetes reports blurry vision. How would you explore this symptom further?', NULL, 'Eye Conditions', 'duration,severity,association,blood sugar,medication');

select * from dbo.QuestionTable
select * from dbo.QuestionTableChild
select * from QuizQn;

--DROP TRIGGER IF EXISTS t_AdultTable_delete;
--GO
--CREATE TRIGGER t_AdultTable_delete ON QuestionTable INSTEAD OF DELETE
--AS BEGIN
--    DECLARE @topic NVARCHAR(255);
--    DECLARE @count int;
--    SELECT @topic = topic FROM deleted;
--    SELECT @count = COUNT(question) FROM QuestionTable WHERE topic = @topic;
--    IF @count = 0
--        DELETE FROM QuestionTable WHERE topic = @topic;
--    ELSE
--        THROW 51000, 'can not delete - QuestionTable is referenced in other tables', 1;
--END;

--DROP TRIGGER IF EXISTS delete_empty_row;
--GO
--CREATE TRIGGER delete_empty_row
--BEFORE DELETE ON QuestionTable
--FOR EACH ROW
--BEGIN
--    IF IS NULL AND NEW.column2 IS NULL AND ... AND NEW.columnN IS NULL THEN
--        DELETE FROM your_table WHERE 
--            column1 = OLD.column1 AND
--            column2 = OLD.column2 AND
--            ... AND
--            columnN = OLD.columnN;
--    END IF;
--END;