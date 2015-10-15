
ALTER TABLE users
alter column created_date date not null

ALTER TABLE attendance
ADD CONSTRAINT fk_student_fee_school
FOREIGN KEY (school_id)
REFERENCES schools(school_id)

delete from attendance

ALTER TABLE students
alter column birthday date not null
