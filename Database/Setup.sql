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
    marks NUMBER(5,2),
    gpa NUMBER(3,2),
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
    assignment_id NUMBER PRIMARY KEY,
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
    v_new_id NUMBER;
BEGIN
    SELECT dept_id INTO v_teacher_dept FROM teachers WHERE teacher_id = p_teacher_id;
    
    SELECT course_type, dept_id INTO v_course_type, v_course_dept FROM courses WHERE course_no = p_course_no;
    
    IF v_teacher_dept != v_course_dept THEN
        RAISE_APPLICATION_ERROR(-20005, 'You can only give assignments for courses in your department.');
    END IF;
    
    IF v_course_type = 'Theory' AND p_assignment_type = 'Project' THEN
        RAISE_APPLICATION_ERROR(-20004, 'Theory classes can only have Assignments, not Projects.');
    END IF;
    
    SELECT NVL(MAX(assignment_id), 0) + 1 INTO v_new_id FROM assignments;
    INSERT INTO assignments (assignment_id, course_no, teacher_id, title, description, assignment_type, due_date)
    VALUES (v_new_id, p_course_no, p_teacher_id, p_title, p_description, p_assignment_type, p_due_date);
    
    COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20006, 'Course or Teacher not found.');
    WHEN OTHERS THEN
        RAISE;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE book_requests CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE books CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

CREATE TABLE books (
    book_id NUMBER PRIMARY KEY,
    title VARCHAR2(200) NOT NULL,
    author VARCHAR2(100) NOT NULL,
    total_copies NUMBER NOT NULL,
    available_copies NUMBER NOT NULL
);
/


CREATE TABLE book_requests (
    request_id NUMBER PRIMARY KEY,
    student_id VARCHAR2(20) NOT NULL,
    book_id NUMBER NOT NULL,
    request_date DATE DEFAULT SYSDATE,
    status VARCHAR2(20) DEFAULT 'Pending' CHECK (status IN ('Pending', 'Approved', 'Rejected', 'Returned')),
    CONSTRAINT fk_br_student FOREIGN KEY (student_id) REFERENCES students(student_id) ON DELETE CASCADE,
    CONSTRAINT fk_br_book FOREIGN KEY (book_id) REFERENCES books(book_id) ON DELETE CASCADE
);
/


CREATE OR REPLACE PROCEDURE add_book (
    p_title IN VARCHAR2,
    p_author IN VARCHAR2,
    p_copies IN NUMBER
)
IS
    v_new_id NUMBER;
BEGIN
    SELECT NVL(MAX(book_id), 0) + 1 INTO v_new_id FROM books;
    INSERT INTO books (book_id, title, author, total_copies, available_copies)
    VALUES (v_new_id, p_title, p_author, p_copies, p_copies);
    COMMIT;
END;
/

CREATE OR REPLACE PROCEDURE request_book (
    p_student_id IN VARCHAR2,
    p_book_id IN NUMBER
)
IS
    v_available NUMBER;
    v_new_id NUMBER;
BEGIN
    SELECT available_copies INTO v_available FROM books WHERE book_id = p_book_id;
    
    IF v_available > 0 THEN
        SELECT NVL(MAX(request_id), 0) + 1 INTO v_new_id FROM book_requests;
        INSERT INTO book_requests (request_id, student_id, book_id) VALUES (v_new_id, p_student_id, p_book_id);
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20007, 'Book is currently out of stock.');
    END IF;
END;
/

CREATE OR REPLACE PROCEDURE approve_book_request (
    p_request_id IN NUMBER
)
IS
    v_book_id NUMBER;
    v_status VARCHAR2(20);
    v_available NUMBER;
BEGIN
    SELECT book_id, status INTO v_book_id, v_status FROM book_requests WHERE request_id = p_request_id;
    
    IF v_status = 'Pending' THEN
        SELECT available_copies INTO v_available FROM books WHERE book_id = v_book_id FOR UPDATE;
        IF v_available > 0 THEN
            UPDATE books SET available_copies = available_copies - 1 WHERE book_id = v_book_id;
            UPDATE book_requests SET status = 'Approved' WHERE request_id = p_request_id;
            COMMIT;
        ELSE
            RAISE_APPLICATION_ERROR(-20008, 'Cannot approve: Book is out of stock.');
        END IF;
    ELSE
        RAISE_APPLICATION_ERROR(-20009, 'Request is not in Pending state.');
    END IF;
END;
/

CREATE OR REPLACE PROCEDURE reject_book_request (
    p_request_id IN NUMBER
)
IS
BEGIN
    UPDATE book_requests SET status = 'Rejected' WHERE request_id = p_request_id AND status = 'Pending';
    COMMIT;
END;
/

CREATE OR REPLACE PROCEDURE return_book (
    p_request_id IN NUMBER
)
IS
    v_book_id NUMBER;
    v_status VARCHAR2(20);
BEGIN
    SELECT book_id, status INTO v_book_id, v_status FROM book_requests WHERE request_id = p_request_id;
    
    IF v_status = 'Approved' THEN
        UPDATE books SET available_copies = available_copies + 1 WHERE book_id = v_book_id;
        UPDATE book_requests SET status = 'Returned' WHERE request_id = p_request_id;
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20010, 'Cannot return: Book was not approved/borrowed.');
    END IF;
END;
/


CREATE OR REPLACE VIEW student_details_view AS
SELECT s.student_id, s.full_name, s.email, s.phone,
       d.dept_name,
       t.first_name || ' ' || t.last_name AS advisor_name
FROM students s
LEFT JOIN department d ON s.dept_id = d.dept_id
LEFT JOIN teachers t ON s.advisor_id = t.teacher_id;
/

CREATE OR REPLACE VIEW course_enrollment_view AS
SELECT c.course_no, c.course_name, d.dept_name, COUNT(sc.student_id) as enrolled_students
FROM courses c
LEFT JOIN department d ON c.dept_id = d.dept_id
LEFT JOIN student_courses sc ON c.course_no = sc.course_no
GROUP BY c.course_no, c.course_name, d.dept_name;
/


CREATE OR REPLACE TRIGGER update_dept_student_count
AFTER INSERT OR DELETE OR UPDATE OF dept_id ON students
FOR EACH ROW
BEGIN
    IF INSERTING THEN
        IF :NEW.dept_id IS NOT NULL THEN
            UPDATE department SET no_of_students = NVL(no_of_students, 0) + 1 WHERE dept_id = :NEW.dept_id;
        END IF;
    ELSIF DELETING THEN
        IF :OLD.dept_id IS NOT NULL THEN
            UPDATE department SET no_of_students = NVL(no_of_students, 0) - 1 WHERE dept_id = :OLD.dept_id;
        END IF;
    ELSIF UPDATING THEN
        IF :OLD.dept_id IS NOT NULL THEN
            UPDATE department SET no_of_students = NVL(no_of_students, 0) - 1 WHERE dept_id = :OLD.dept_id;
        END IF;
        IF :NEW.dept_id IS NOT NULL THEN
            UPDATE department SET no_of_students = NVL(no_of_students, 0) + 1 WHERE dept_id = :NEW.dept_id;
        END IF;
    END IF;
END;
/

-- Trigger to auto-format student names (capitalize first letters)
CREATE OR REPLACE TRIGGER format_student_name
BEFORE INSERT OR UPDATE ON students
FOR EACH ROW
BEGIN
    :NEW.full_name := INITCAP(:NEW.full_name);
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE research_requests CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

CREATE TABLE research_requests (
    request_id NUMBER PRIMARY KEY,
    student_id VARCHAR2(20) NOT NULL,
    teacher_id VARCHAR2(20) NOT NULL,
    research_interest VARCHAR2(500),
    research_description VARCHAR2(2000),
    previous_experience VARCHAR2(2000),
    request_date DATE DEFAULT SYSDATE,
    status VARCHAR2(20) DEFAULT 'Pending' CHECK (status IN ('Pending', 'Accepted', 'Rejected')),
    CONSTRAINT fk_rr_student FOREIGN KEY (student_id) REFERENCES students(student_id) ON DELETE CASCADE,
    CONSTRAINT fk_rr_teacher FOREIGN KEY (teacher_id) REFERENCES teachers(teacher_id) ON DELETE CASCADE
);
/

CREATE OR REPLACE PROCEDURE request_research (
    p_student_id IN VARCHAR2,
    p_teacher_id IN VARCHAR2,
    p_research_interest IN VARCHAR2,
    p_research_description IN VARCHAR2,
    p_previous_experience IN VARCHAR2
)
IS
    v_new_id NUMBER;
    v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM research_requests WHERE student_id = p_student_id AND status = 'Pending';
    IF v_count > 0 THEN
        RAISE_APPLICATION_ERROR(-20011, 'You already have a pending research request.');
    END IF;

    SELECT NVL(MAX(request_id), 0) + 1 INTO v_new_id FROM research_requests;
    INSERT INTO research_requests (request_id, student_id, teacher_id, research_interest, research_description, previous_experience) 
    VALUES (v_new_id, p_student_id, p_teacher_id, p_research_interest, p_research_description, p_previous_experience);
    COMMIT;
END;
/

CREATE OR REPLACE PROCEDURE accept_research_request (
    p_request_id IN NUMBER
)
IS
    v_student_id VARCHAR2(20);
    v_teacher_id VARCHAR2(20);
    v_status VARCHAR2(20);
BEGIN
    SELECT student_id, teacher_id, status INTO v_student_id, v_teacher_id, v_status FROM research_requests WHERE request_id = p_request_id;
    
    IF v_status = 'Pending' THEN
        UPDATE research_requests SET status = 'Accepted' WHERE request_id = p_request_id;
        UPDATE students SET advisor_id = v_teacher_id WHERE student_id = v_student_id;
        COMMIT;
    ELSE
        RAISE_APPLICATION_ERROR(-20012, 'Request is not in Pending state.');
    END IF;
END;
/

CREATE OR REPLACE PROCEDURE reject_research_request (
    p_request_id IN NUMBER
)
IS
BEGIN
    UPDATE research_requests SET status = 'Rejected' WHERE request_id = p_request_id AND status = 'Pending';
    COMMIT;
END;
/

CREATE OR REPLACE PROCEDURE get_teacher_advisees (
    p_teacher_id IN students.advisor_id%TYPE,
    p_recordset OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_recordset FOR
        SELECT student_id, full_name, email, phone, dept_id, address, advisor_id, password
        FROM students
        WHERE advisor_id = p_teacher_id;
END;
/

CREATE OR REPLACE PROCEDURE generate_department_report (
    p_dept_id IN department.dept_id%TYPE
)
IS
    CURSOR c_students IS
        SELECT full_name, email
        FROM students
        WHERE dept_id = p_dept_id;
    
    v_full_name students.full_name%TYPE;
    v_email students.email%TYPE;
    v_count NUMBER := 0;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--- Student Report for Department: ' || p_dept_id || ' ---');
    
    OPEN c_students;
    LOOP
        FETCH c_students INTO v_full_name, v_email;
        EXIT WHEN c_students%NOTFOUND;
        
        DBMS_OUTPUT.PUT_LINE('Student: ' || v_full_name || ' | Email: ' || v_email);
        v_count := v_count + 1;
    END LOOP;
    CLOSE c_students;
    
    DBMS_OUTPUT.PUT_LINE('Total Students: ' || v_count);
END;
/

-- Procedure to assign marks and calculate GPA
CREATE OR REPLACE PROCEDURE assign_marks (
    p_student_id IN VARCHAR2,
    p_course_no IN VARCHAR2,
    p_marks IN NUMBER
)
IS
    v_gpa NUMBER(3,2);
BEGIN
    IF p_marks >= 80 THEN v_gpa := 4.00;
    ELSIF p_marks >= 75 THEN v_gpa := 3.75;
    ELSIF p_marks >= 70 THEN v_gpa := 3.50;
    ELSIF p_marks >= 65 THEN v_gpa := 3.25;
    ELSIF p_marks >= 60 THEN v_gpa := 3.00;
    ELSIF p_marks >= 55 THEN v_gpa := 2.75;
    ELSIF p_marks >= 50 THEN v_gpa := 2.50;
    ELSIF p_marks >= 45 THEN v_gpa := 2.25;
    ELSIF p_marks >= 40 THEN v_gpa := 2.00;
    ELSE v_gpa := 0.00;
    END IF;

    UPDATE student_courses 
    SET marks = p_marks, gpa = v_gpa 
    WHERE student_id = p_student_id AND course_no = p_course_no;
    
    COMMIT;
END;
/

-- Attendance Tracking System
CREATE TABLE attendance (
    attendance_id NUMBER PRIMARY KEY,
    student_id VARCHAR2(20),
    course_no VARCHAR2(20),
    attendance_date DATE,
    status VARCHAR2(10) CHECK (status IN ('Present', 'Absent')),
    CONSTRAINT fk_att_student FOREIGN KEY (student_id) REFERENCES students(student_id) ON DELETE CASCADE,
    CONSTRAINT fk_att_course FOREIGN KEY (course_no) REFERENCES courses(course_no) ON DELETE CASCADE,
    CONSTRAINT uq_att_record UNIQUE (student_id, course_no, attendance_date)
);
/

-- Sequence equivalent for attendance_id (handled in procedure)
CREATE OR REPLACE PROCEDURE mark_attendance (
    p_student_id IN VARCHAR2,
    p_course_no IN VARCHAR2,
    p_date IN DATE,
    p_status IN VARCHAR2
)
IS
    v_count NUMBER;
    v_new_id NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM attendance 
    WHERE student_id = p_student_id AND course_no = p_course_no AND attendance_date = p_date;
    
    IF v_count > 0 THEN
        UPDATE attendance SET status = p_status
        WHERE student_id = p_student_id AND course_no = p_course_no AND attendance_date = p_date;
    ELSE
        SELECT NVL(MAX(attendance_id), 0) + 1 INTO v_new_id FROM attendance;
        INSERT INTO attendance (attendance_id, student_id, course_no, attendance_date, status)
        VALUES (v_new_id, p_student_id, p_course_no, p_date, p_status);
    END IF;
    COMMIT;
END;
/

-- PL/SQL Function to calculate attendance percentage
CREATE OR REPLACE FUNCTION get_attendance_pct (
    p_student_id IN VARCHAR2,
    p_course_no IN VARCHAR2
) RETURN NUMBER IS
    v_total NUMBER := 0;
    v_present NUMBER := 0;
BEGIN
    SELECT COUNT(*) INTO v_total FROM attendance 
    WHERE student_id = p_student_id AND course_no = p_course_no;
    
    IF v_total = 0 THEN 
        RETURN 0; 
    END IF;
    
    SELECT COUNT(*) INTO v_present FROM attendance 
    WHERE student_id = p_student_id AND course_no = p_course_no AND status = 'Present';
    
    RETURN ROUND((v_present / v_total) * 100, 2);
END;
/
