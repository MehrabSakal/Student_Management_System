SET SERVEROUTPUT ON;

CREATE TABLE teachers (
    teacher_id VARCHAR2(20) PRIMARY KEY,
    first_name VARCHAR2(100) NOT NULL,
    last_name VARCHAR2(100) NOT NULL,
    email VARCHAR2(100),
    designation VARCHAR2(100),
    department VARCHAR2(50)
);
/

CREATE OR REPLACE PROCEDURE add_teacher (
    p_teacher_id IN teachers.teacher_id%TYPE,
    p_first_name IN teachers.first_name%TYPE,
    p_last_name IN teachers.last_name%TYPE,
    p_email IN teachers.email%TYPE,
    p_designation IN teachers.designation%TYPE,
    p_department IN teachers.department%TYPE
)
IS
BEGIN
    INSERT INTO teachers (teacher_id, first_name, last_name, email, designation, department)
    VALUES (p_teacher_id, p_first_name, p_last_name, p_email, p_designation, p_department);
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Teacher added successfully!');
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        DBMS_OUTPUT.PUT_LINE('Error: A teacher with this ID already exists.');
        RAISE;
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('An error occurred while adding the teacher.');
        RAISE;
END;
/

CREATE OR REPLACE PROCEDURE update_teacher (
    p_teacher_id IN teachers.teacher_id%TYPE,
    p_first_name IN teachers.first_name%TYPE,
    p_last_name IN teachers.last_name%TYPE,
    p_email IN teachers.email%TYPE,
    p_designation IN teachers.designation%TYPE,
    p_department IN teachers.department%TYPE
)
IS
BEGIN
    UPDATE teachers
    SET first_name = p_first_name,
        last_name = p_last_name,
        email = p_email,
        designation = p_designation,
        department = p_department
    WHERE teacher_id = p_teacher_id;
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Teacher updated successfully!');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('An error occurred while updating the teacher.');
        RAISE;
END;
/

CREATE OR REPLACE PROCEDURE delete_teacher (
    p_teacher_id IN teachers.teacher_id%TYPE
)
IS
BEGIN
    DELETE FROM teachers
    WHERE teacher_id = p_teacher_id;
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Teacher deleted successfully!');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('An error occurred while deleting the teacher.');
        RAISE;
END;
/

CREATE TABLE students (
    student_id VARCHAR2(20) PRIMARY KEY,
    full_name VARCHAR2(100) NOT NULL,
    email VARCHAR2(100),
    phone VARCHAR2(20),
    department VARCHAR2(50),
    address VARCHAR2(200),
    advisor_id VARCHAR2(20),
    CONSTRAINT fk_advisor FOREIGN KEY (advisor_id) REFERENCES teachers(teacher_id) ON DELETE SET NULL
);
/

CREATE OR REPLACE PROCEDURE add_student (
    p_student_id IN students.student_id%TYPE,
    p_full_name IN students.full_name%TYPE,
    p_email IN students.email%TYPE,
    p_phone IN students.phone%TYPE,
    p_department IN students.department%TYPE,
    p_address IN students.address%TYPE,
    p_advisor_id IN students.advisor_id%TYPE
)
IS
BEGIN
    INSERT INTO students (student_id, full_name, email, phone, department, address, advisor_id)
    VALUES (p_student_id, p_full_name, p_email, p_phone, p_department, p_address, p_advisor_id);
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Student added successfully!');
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        DBMS_OUTPUT.PUT_LINE('Error: A student with this ID already exists.');
        RAISE;
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('An error occurred while adding the student.');
        RAISE;
END;
/

CREATE OR REPLACE PROCEDURE update_student (
    p_student_id IN students.student_id%TYPE,
    p_full_name IN students.full_name%TYPE,
    p_email IN students.email%TYPE,
    p_phone IN students.phone%TYPE,
    p_department IN students.department%TYPE,
    p_address IN students.address%TYPE,
    p_advisor_id IN students.advisor_id%TYPE
)
IS
BEGIN
    UPDATE students
    SET full_name = p_full_name,
        email = p_email,
        phone = p_phone,
        department = p_department,
        address = p_address,
        advisor_id = p_advisor_id
    WHERE student_id = p_student_id;
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Student updated successfully!');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('An error occurred while updating the student.');
        RAISE;
END;
/

CREATE OR REPLACE PROCEDURE delete_student (
    p_student_id IN students.student_id%TYPE
)
IS
BEGIN
    DELETE FROM students
    WHERE student_id = p_student_id;
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Student deleted successfully!');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('An error occurred while deleting the student.');
        RAISE;
END;
/
