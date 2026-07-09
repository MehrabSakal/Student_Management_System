SET SERVEROUTPUT ON;

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE student_courses CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE students CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE courses CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE assignments CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE teachers CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE department CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

CREATE TABLE department (
    dept_id VARCHAR2(20) PRIMARY KEY,
    dept_name VARCHAR2(100) NOT NULL,
    faculty VARCHAR2(100),
    no_of_students NUMBER
);
/

CREATE OR REPLACE PROCEDURE add_department (
    p_dept_id IN department.dept_id%TYPE,
    p_dept_name IN department.dept_name%TYPE,
    p_faculty IN department.faculty%TYPE,
    p_no_of_students IN department.no_of_students%TYPE
)
IS
BEGIN
    INSERT INTO department (dept_id, dept_name, faculty, no_of_students)
    VALUES (p_dept_id, p_dept_name, p_faculty, p_no_of_students);
    
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Department added successfully!');
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        DBMS_OUTPUT.PUT_LINE('Error: A department with this ID already exists.');
        RAISE;
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('An error occurred while adding the department.');
        RAISE;
END;
/

CREATE TABLE teachers (
    teacher_id VARCHAR2(20) PRIMARY KEY,
    first_name VARCHAR2(100) NOT NULL,
    last_name VARCHAR2(100) NOT NULL,
    email VARCHAR2(100),
    designation VARCHAR2(100),
    dept_id VARCHAR2(20),
    password VARCHAR2(100),
    CONSTRAINT fk_teacher_dept FOREIGN KEY (dept_id) REFERENCES department(dept_id) ON DELETE SET NULL
);
/

CREATE OR REPLACE PROCEDURE add_teacher (
    p_teacher_id IN teachers.teacher_id%TYPE,
    p_first_name IN teachers.first_name%TYPE,
    p_last_name IN teachers.last_name%TYPE,
    p_email IN teachers.email%TYPE,
    p_designation IN teachers.designation%TYPE,
    p_dept_id IN teachers.dept_id%TYPE,
    p_password IN teachers.password%TYPE
)
IS
BEGIN
    INSERT INTO teachers (teacher_id, first_name, last_name, email, designation, dept_id, password)
    VALUES (p_teacher_id, p_first_name, p_last_name, p_email, p_designation, p_dept_id, p_password);
    
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
    p_dept_id IN teachers.dept_id%TYPE,
    p_password IN teachers.password%TYPE
)
IS
BEGIN
    UPDATE teachers
    SET first_name = p_first_name,
        last_name = p_last_name,
        email = p_email,
        designation = p_designation,
        dept_id = p_dept_id,
        password = p_password
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
    dept_id VARCHAR2(20),
    address VARCHAR2(200),
    advisor_id VARCHAR2(20),
    password VARCHAR2(100),
    CONSTRAINT fk_student_dept FOREIGN KEY (dept_id) REFERENCES department(dept_id) ON DELETE SET NULL,
    CONSTRAINT fk_advisor FOREIGN KEY (advisor_id) REFERENCES teachers(teacher_id) ON DELETE SET NULL
);
/

CREATE OR REPLACE PROCEDURE add_student (
    p_student_id IN students.student_id%TYPE,
    p_full_name IN students.full_name%TYPE,
    p_email IN students.email%TYPE,
    p_phone IN students.phone%TYPE,
    p_dept_id IN students.dept_id%TYPE,
    p_address IN students.address%TYPE,
    p_advisor_id IN students.advisor_id%TYPE,
    p_password IN students.password%TYPE
)
IS
BEGIN
    INSERT INTO students (student_id, full_name, email, phone, dept_id, address, advisor_id, password)
    VALUES (p_student_id, p_full_name, p_email, p_phone, p_dept_id, p_address, p_advisor_id, p_password);
    
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
    p_dept_id IN students.dept_id%TYPE,
    p_address IN students.address%TYPE,
    p_advisor_id IN students.advisor_id%TYPE,
    p_password IN students.password%TYPE
)
IS
BEGIN
    UPDATE students
    SET full_name = p_full_name,
        email = p_email,
        phone = p_phone,
        dept_id = p_dept_id,
        address = p_address,
        advisor_id = p_advisor_id,
        password = p_password
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

CREATE TABLE courses (
    course_no VARCHAR2(20) PRIMARY KEY,
    course_name VARCHAR2(100) NOT NULL,
    dept_id VARCHAR2(20),
    course_type VARCHAR2(20) CHECK (course_type IN ('Theory', 'Lab')),
    CONSTRAINT fk_course_dept FOREIGN KEY (dept_id) REFERENCES department(dept_id) ON DELETE SET NULL
);
/

CREATE TABLE student_courses (
    student_id VARCHAR2(20),
    course_no VARCHAR2(20),
    PRIMARY KEY (student_id, course_no),
    CONSTRAINT fk_enroll_student FOREIGN KEY (student_id) REFERENCES students(student_id) ON DELETE CASCADE,
    CONSTRAINT fk_enroll_course FOREIGN KEY (course_no) REFERENCES courses(course_no) ON DELETE CASCADE
);
/

CREATE OR REPLACE PROCEDURE add_course (
    p_course_no IN courses.course_no%TYPE,
    p_course_name IN courses.course_name%TYPE,
    p_dept_id IN courses.dept_id%TYPE,
    p_course_type IN courses.course_type%TYPE
)
IS
BEGIN
    INSERT INTO courses (course_no, course_name, dept_id, course_type)
    VALUES (p_course_no, p_course_name, p_dept_id, p_course_type);
    
    COMMIT;
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        RAISE_APPLICATION_ERROR(-20003, 'A course with this NO already exists.');
    WHEN OTHERS THEN
        RAISE;
END;
/

CREATE OR REPLACE PROCEDURE register_course (
    p_student_id IN student_courses.student_id%TYPE,
    p_course_no IN student_courses.course_no%TYPE
)
IS
    v_course_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_course_count
    FROM student_courses
    WHERE student_id = p_student_id;
    
    IF v_course_count >= 5 THEN
        RAISE_APPLICATION_ERROR(-20001, 'Student cannot register for more than 5 courses.');
    END IF;

    INSERT INTO student_courses (student_id, course_no)
    VALUES (p_student_id, p_course_no);
    
    COMMIT;
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        RAISE_APPLICATION_ERROR(-20002, 'Student is already registered for this course.');
    WHEN OTHERS THEN
        RAISE;
END;
/

CREATE TABLE assignments (
    assignment_id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    course_no VARCHAR2(20) NOT NULL,
    teacher_id VARCHAR2(20) NOT NULL,
    title VARCHAR2(100) NOT NULL,
    description VARCHAR2(500),
    assignment_type VARCHAR2(20) CHECK (assignment_type IN ('Assignment', 'Project')),
    due_date DATE,
    CONSTRAINT fk_assignment_course FOREIGN KEY (course_no) REFERENCES courses(course_no) ON DELETE CASCADE,
    CONSTRAINT fk_assignment_teacher FOREIGN KEY (teacher_id) REFERENCES teachers(teacher_id) ON DELETE CASCADE
);
/

CREATE OR REPLACE PROCEDURE add_assignment (
    p_course_no IN assignments.course_no%TYPE,
    p_teacher_id IN assignments.teacher_id%TYPE,
    p_title IN assignments.title%TYPE,
    p_description IN assignments.description%TYPE,
    p_assignment_type IN assignments.assignment_type%TYPE,
    p_due_date IN assignments.due_date%TYPE
)
IS
    v_course_type courses.course_type%TYPE;
    v_teacher_dept teachers.dept_id%TYPE;
    v_course_dept courses.dept_id%TYPE;
BEGIN
    -- Verify teacher exists and get department
    SELECT dept_id INTO v_teacher_dept FROM teachers WHERE teacher_id = p_teacher_id;
    
    -- Verify course exists and get type and department
    SELECT course_type, dept_id INTO v_course_type, v_course_dept FROM courses WHERE course_no = p_course_no;
    
    -- Validate department (teacher can only give assignments for courses in their department)
    IF v_teacher_dept != v_course_dept THEN
        RAISE_APPLICATION_ERROR(-20005, 'You can only give assignments for courses in your department.');
    END IF;
    
    -- Validate assignment type based on course type
    IF v_course_type = 'Theory' AND p_assignment_type = 'Project' THEN
        RAISE_APPLICATION_ERROR(-20004, 'Theory classes can only have Assignments, not Projects.');
    END IF;
    
    -- Insert the assignment
    INSERT INTO assignments (course_no, teacher_id, title, description, assignment_type, due_date)
    VALUES (p_course_no, p_teacher_id, p_title, p_description, p_assignment_type, p_due_date);
    
    COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20006, 'Course or Teacher not found.');
    WHEN OTHERS THEN
        RAISE;
END;
/
