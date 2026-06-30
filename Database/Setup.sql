SET SERVEROUTPUT ON;

CREATE TABLE students (
    student_id VARCHAR2(20) PRIMARY KEY,
    full_name VARCHAR2(100) NOT NULL,
    email VARCHAR2(100),
    phone VARCHAR2(20),
    department VARCHAR2(50),
    address VARCHAR2(200),
    advisor_id VARCHAR2(20)
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
