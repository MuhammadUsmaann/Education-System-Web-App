USE [education_system]
GO
/****** Object:  Table [dbo].[admins]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admins](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](255) NOT NULL,
	[last_name] [nvarchar](255) NOT NULL,
	[middle_name] [nvarchar](255) NULL,
	[gender] [nvarchar](250) NULL,
	[updated_date] [date] NOT NULL,
	[other_detail] [text] NULL,
	[created_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[user_id] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[address] [nvarchar](255) NULL,
	[phone] [nvarchar](255) NULL,
	[birthday] [date] NULL,
	[landline] [nvarchar](50) NULL,
	[mother_name] [nvarchar](250) NULL,
	[father_name] [nvarchar](250) NULL,
	[cnic] [nvarchar](250) NULL,
	[sys_id] [nvarchar](250) NOT NULL,
	[email] [nvarchar](250) NULL,
 CONSTRAINT [PK_users_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[attendance]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[attendance](
	[attendance_id] [int] IDENTITY(1,1) NOT NULL,
	[date] [nvarchar](50) NOT NULL,
	[student_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[school_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_attendance] PRIMARY KEY CLUSTERED 
(
	[attendance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[classes]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[classes](
	[class_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[numeric_num] [int] NOT NULL,
	[description] [nvarchar](255) NULL,
	[school_id] [int] NOT NULL,
	[teacher_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_class] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[exams]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exams](
	[exam_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[comment] [nvarchar](255) NOT NULL,
	[date] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
	[school_id] [int] NULL,
 CONSTRAINT [PK_exams] PRIMARY KEY CLUSTERED 
(
	[exam_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[fee]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fee](
	[fee_id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[fee] [int] NOT NULL,
	[school_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL,
 CONSTRAINT [PK_fee] PRIMARY KEY CLUSTERED 
(
	[fee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[parents]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[parents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gfirst_name] [nvarchar](255) NOT NULL,
	[glast_name] [nvarchar](255) NOT NULL,
	[gmiddle_name] [nvarchar](255) NULL,
	[gender] [nvarchar](250) NULL,
	[updated_date] [date] NOT NULL,
	[other_detail] [text] NULL,
	[school_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[user_id] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[address] [nvarchar](255) NULL,
	[phone] [nvarchar](255) NULL,
	[profession] [nvarchar](255) NULL,
	[birthday] [date] NULL,
	[income] [int] NULL,
	[landline] [nvarchar](50) NULL,
	[pfirst_name] [nvarchar](250) NULL,
	[plast_name] [nvarchar](250) NULL,
	[mother_name] [nvarchar](250) NULL,
	[father_name] [nvarchar](250) NULL,
	[cnic] [nvarchar](250) NULL,
	[sys_id] [nvarchar](250) NOT NULL,
	[email] [nvarchar](250) NULL,
 CONSTRAINT [PK_parents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[roles]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_description] [nvarchar](255) NULL,
	[role_code] [nvarchar](255) NULL,
	[sys_id] [nvarchar](255) NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[schools]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[schools](
	[school_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](250) NOT NULL,
	[phone] [nvarchar](250) NOT NULL,
	[city_id] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[code] [nvarchar](50) NULL,
	[owner_id] [int] NOT NULL,
 CONSTRAINT [PK_cl_school] PRIMARY KEY CLUSTERED 
(
	[school_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[session]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[session](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[start_date] [nvarchar](250) NOT NULL,
	[end_date] [nvarchar](250) NOT NULL,
	[isDefault] [nvarchar](250) NOT NULL,
	[school_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_by] [int] NOT NULL,
	[updated_date] [date] NOT NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_session] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[student_fee]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student_fee](
	[student_fee_id] [int] IDENTITY(1,1) NOT NULL,
	[paid_status] [nvarchar](50) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[paid_fee] [int] NULL,
	[month] [nvarchar](50) NULL,
	[paid_date] [date] NULL,
	[fee] [int] NOT NULL,
	[student_id] [int] NOT NULL,
	[school_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[session_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](255) NOT NULL,
	[last_name] [nvarchar](255) NOT NULL,
	[gender] [nvarchar](255) NOT NULL,
	[user_id] [int] NOT NULL,
	[address] [nvarchar](255) NULL,
	[phone] [nvarchar](255) NULL,
	[roll] [nvarchar](255) NOT NULL,
	[school_id] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[parent_id] [int] NOT NULL,
	[birthday] [date] NOT NULL,
	[monthly_fee] [int] NOT NULL,
	[discount] [int] NOT NULL,
	[examination_fee] [int] NOT NULL,
	[admission_fee] [int] NOT NULL,
	[other_charges] [int] NOT NULL,
	[sys_id] [nvarchar](250) NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL,
 CONSTRAINT [PK_students] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[subject_exam]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subject_exam](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subject_id] [int] NOT NULL,
	[exam_id] [int] NOT NULL,
	[school_id] [int] NOT NULL,
	[marks] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL,
	[class_id] [int] NOT NULL,
	[comment] [nvarchar](255) NULL,
 CONSTRAINT [PK_subject_exam] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[subject_marks]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subject_marks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subject_id] [int] NOT NULL,
	[exam_id] [int] NOT NULL,
	[school_id] [int] NOT NULL,
	[marks] [int] NOT NULL,
	[class_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL,
	[comment] [nvarchar](255) NULL,
	[student_id] [int] NULL,
 CONSTRAINT [PK_subject_marks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[subjects]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subjects](
	[subject_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[teacher_id] [int] NOT NULL,
	[school_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_date] [date] NOT NULL,
	[class_id] [int] NOT NULL,
	[pass_marks] [int] NOT NULL,
	[total_marks] [int] NOT NULL,
 CONSTRAINT [PK_subjects] PRIMARY KEY CLUSTERED 
(
	[subject_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[teachers]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[teachers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](255) NOT NULL,
	[last_name] [nvarchar](255) NOT NULL,
	[middle_name] [nvarchar](255) NULL,
	[gender] [nvarchar](250) NULL,
	[updated_date] [date] NOT NULL,
	[other_detail] [text] NULL,
	[school_id] [int] NOT NULL,
	[created_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[user_id] [int] NOT NULL,
	[updated_by] [int] NOT NULL,
	[address] [nvarchar](255) NULL,
	[phone] [nvarchar](255) NULL,
	[birthday] [date] NULL,
	[salary] [int] NULL,
	[landline] [nvarchar](50) NULL,
	[mother_name] [nvarchar](250) NULL,
	[father_name] [nvarchar](250) NULL,
	[cnic] [nvarchar](250) NULL,
	[sys_id] [nvarchar](250) NOT NULL,
	[email] [nvarchar](250) NULL,
 CONSTRAINT [PK_teachers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 10/15/2015 9:38:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[role_code] [nvarchar](255) NOT NULL,
	[updated_date] [date] NOT NULL,
	[created_by] [int] NOT NULL,
	[created_date] [date] NOT NULL,
	[updated_by] [int] NOT NULL,
	[status] [nvarchar](10) NOT NULL,
	[school_id] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[classes] ADD  CONSTRAINT [DF_classes_status]  DEFAULT ('00001') FOR [status]
GO
ALTER TABLE [dbo].[fee] ADD  CONSTRAINT [DF_fee_fee]  DEFAULT ((0)) FOR [fee]
GO
ALTER TABLE [dbo].[fee] ADD  CONSTRAINT [DF_fee_school_id]  DEFAULT ((0)) FOR [school_id]
GO
ALTER TABLE [dbo].[fee] ADD  CONSTRAINT [DF_fee_class_id]  DEFAULT ((0)) FOR [class_id]
GO
ALTER TABLE [dbo].[schools] ADD  CONSTRAINT [DF_schools_owner_id]  DEFAULT ((0)) FOR [owner_id]
GO
ALTER TABLE [dbo].[student_fee] ADD  CONSTRAINT [DF_student_fee_paid_fee]  DEFAULT ((0)) FOR [paid_fee]
GO
ALTER TABLE [dbo].[student_fee] ADD  CONSTRAINT [DF_student_fee_month]  DEFAULT ((0)) FOR [month]
GO
ALTER TABLE [dbo].[student_fee] ADD  CONSTRAINT [DF_student_fee_month_fee]  DEFAULT ((0)) FOR [fee]
GO
ALTER TABLE [dbo].[students] ADD  CONSTRAINT [DF_students_monthly_fee]  DEFAULT ((0)) FOR [monthly_fee]
GO
ALTER TABLE [dbo].[students] ADD  CONSTRAINT [DF_students_discount]  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[students] ADD  CONSTRAINT [DF_students_examination_fee]  DEFAULT ((0)) FOR [examination_fee]
GO
ALTER TABLE [dbo].[students] ADD  CONSTRAINT [DF_students_admission_fee]  DEFAULT ((0)) FOR [admission_fee]
GO
ALTER TABLE [dbo].[students] ADD  CONSTRAINT [DF_students_other_charges]  DEFAULT ((0)) FOR [other_charges]
GO
ALTER TABLE [dbo].[subjects] ADD  CONSTRAINT [DF_subjects_pass_marks]  DEFAULT ((0)) FOR [pass_marks]
GO
ALTER TABLE [dbo].[subjects] ADD  CONSTRAINT [DF_subjects_total+marks]  DEFAULT ((0)) FOR [total_marks]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_school_id]  DEFAULT ((0)) FOR [school_id]
GO
ALTER TABLE [dbo].[admins]  WITH CHECK ADD  CONSTRAINT [fk_users_details_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[admins] CHECK CONSTRAINT [fk_users_details_users]
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [fk_attendance_class] FOREIGN KEY([class_id])
REFERENCES [dbo].[classes] ([class_id])
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [fk_attendance_class]
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [fk_attendance_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [fk_attendance_created_by]
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [fk_attendance_school] FOREIGN KEY([school_id])
REFERENCES [dbo].[schools] ([school_id])
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [fk_attendance_school]
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [fk_attendance_student] FOREIGN KEY([student_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [fk_attendance_student]
GO
ALTER TABLE [dbo].[attendance]  WITH CHECK ADD  CONSTRAINT [fk_attendance_updated_by] FOREIGN KEY([updated_by])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[attendance] CHECK CONSTRAINT [fk_attendance_updated_by]
GO
ALTER TABLE [dbo].[classes]  WITH CHECK ADD  CONSTRAINT [fk_classes_schoolss] FOREIGN KEY([school_id])
REFERENCES [dbo].[schools] ([school_id])
GO
ALTER TABLE [dbo].[classes] CHECK CONSTRAINT [fk_classes_schoolss]
GO
ALTER TABLE [dbo].[classes]  WITH CHECK ADD  CONSTRAINT [fk_classes_users] FOREIGN KEY([teacher_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[classes] CHECK CONSTRAINT [fk_classes_users]
GO
ALTER TABLE [dbo].[parents]  WITH CHECK ADD  CONSTRAINT [fk_parents_schools] FOREIGN KEY([school_id])
REFERENCES [dbo].[schools] ([school_id])
GO
ALTER TABLE [dbo].[parents] CHECK CONSTRAINT [fk_parents_schools]
GO
ALTER TABLE [dbo].[parents]  WITH CHECK ADD  CONSTRAINT [fk_parents_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[parents] CHECK CONSTRAINT [fk_parents_users]
GO
ALTER TABLE [dbo].[student_fee]  WITH CHECK ADD  CONSTRAINT [fk_student_fee_classes] FOREIGN KEY([class_id])
REFERENCES [dbo].[classes] ([class_id])
GO
ALTER TABLE [dbo].[student_fee] CHECK CONSTRAINT [fk_student_fee_classes]
GO
ALTER TABLE [dbo].[student_fee]  WITH CHECK ADD  CONSTRAINT [fk_student_fee_created_by] FOREIGN KEY([updated_by])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[student_fee] CHECK CONSTRAINT [fk_student_fee_created_by]
GO
ALTER TABLE [dbo].[student_fee]  WITH CHECK ADD  CONSTRAINT [fk_student_fee_school] FOREIGN KEY([school_id])
REFERENCES [dbo].[schools] ([school_id])
GO
ALTER TABLE [dbo].[student_fee] CHECK CONSTRAINT [fk_student_fee_school]
GO
ALTER TABLE [dbo].[student_fee]  WITH CHECK ADD  CONSTRAINT [fk_student_fee_session] FOREIGN KEY([session_id])
REFERENCES [dbo].[session] ([session_id])
GO
ALTER TABLE [dbo].[student_fee] CHECK CONSTRAINT [fk_student_fee_session]
GO
ALTER TABLE [dbo].[student_fee]  WITH CHECK ADD  CONSTRAINT [fk_student_fee_student] FOREIGN KEY([student_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[student_fee] CHECK CONSTRAINT [fk_student_fee_student]
GO
ALTER TABLE [dbo].[student_fee]  WITH CHECK ADD  CONSTRAINT [fk_student_fee_updated_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[student_fee] CHECK CONSTRAINT [fk_student_fee_updated_by]
GO
ALTER TABLE [dbo].[students]  WITH CHECK ADD  CONSTRAINT [fk_students_classes] FOREIGN KEY([class_id])
REFERENCES [dbo].[classes] ([class_id])
GO
ALTER TABLE [dbo].[students] CHECK CONSTRAINT [fk_students_classes]
GO
ALTER TABLE [dbo].[students]  WITH CHECK ADD  CONSTRAINT [fk_students_parent] FOREIGN KEY([parent_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[students] CHECK CONSTRAINT [fk_students_parent]
GO
ALTER TABLE [dbo].[students]  WITH CHECK ADD  CONSTRAINT [fk_students_school] FOREIGN KEY([school_id])
REFERENCES [dbo].[schools] ([school_id])
GO
ALTER TABLE [dbo].[students] CHECK CONSTRAINT [fk_students_school]
GO
ALTER TABLE [dbo].[students]  WITH CHECK ADD  CONSTRAINT [fk_students_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[students] CHECK CONSTRAINT [fk_students_users]
GO
ALTER TABLE [dbo].[subjects]  WITH CHECK ADD  CONSTRAINT [fk_subjects_class] FOREIGN KEY([class_id])
REFERENCES [dbo].[classes] ([class_id])
GO
ALTER TABLE [dbo].[subjects] CHECK CONSTRAINT [fk_subjects_class]
GO
ALTER TABLE [dbo].[subjects]  WITH CHECK ADD  CONSTRAINT [fk_subjects_created_by] FOREIGN KEY([created_by])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[subjects] CHECK CONSTRAINT [fk_subjects_created_by]
GO
ALTER TABLE [dbo].[subjects]  WITH CHECK ADD  CONSTRAINT [fk_subjects_school] FOREIGN KEY([school_id])
REFERENCES [dbo].[schools] ([school_id])
GO
ALTER TABLE [dbo].[subjects] CHECK CONSTRAINT [fk_subjects_school]
GO
ALTER TABLE [dbo].[subjects]  WITH CHECK ADD  CONSTRAINT [fk_subjects_teacher] FOREIGN KEY([teacher_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[subjects] CHECK CONSTRAINT [fk_subjects_teacher]
GO
ALTER TABLE [dbo].[subjects]  WITH CHECK ADD  CONSTRAINT [fk_subjects_updated_by] FOREIGN KEY([updated_by])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[subjects] CHECK CONSTRAINT [fk_subjects_updated_by]
GO
ALTER TABLE [dbo].[teachers]  WITH CHECK ADD  CONSTRAINT [fk_teachers_schools] FOREIGN KEY([school_id])
REFERENCES [dbo].[schools] ([school_id])
GO
ALTER TABLE [dbo].[teachers] CHECK CONSTRAINT [fk_teachers_schools]
GO
ALTER TABLE [dbo].[teachers]  WITH CHECK ADD  CONSTRAINT [fk_teachers_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[teachers] CHECK CONSTRAINT [fk_teachers_users]
GO
