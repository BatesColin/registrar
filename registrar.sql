-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 18, 2018 at 01:28 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `registrar`
--
CREATE DATABASE IF NOT EXISTS `registrar` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `registrar`;

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `id` int(11) NOT NULL,
  `enroll_date` varchar(255) NOT NULL,
  `course_name` varchar(255) NOT NULL,
  `course_number` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`id`, `enroll_date`, `course_name`, `course_number`) VALUES
(1, '2018-07-19', 'MAT', 100),
(2, '2018-07-19', 'LOL', 420),
(3, '2018-07-19', 'HAHA', 123),
(4, '2018-07-19', 'XD', 73),
(5, '2018-07-19', 'noscope', 360);

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`id`, `name`) VALUES
(1, 'Swati'),
(2, 'Anusone'),
(3, 'Naruto'),
(4, 'Sasuke'),
(5, 'Sakura'),
(6, 'Kakashi'),
(7, 'Shikamaru'),
(8, 'Ino'),
(9, 'Choji'),
(10, 'Neji'),
(11, 'Rock Lee'),
(12, 'Might Guy'),
(13, 'Tenten'),
(14, 'Sinchen'),
(15, 'Nick'),
(16, 'Hinata'),
(17, 'Jiraiyah'),
(18, 'Kurama'),
(19, 'Gaara'),
(20, 'Nagato'),
(21, 'Madara'),
(22, 'Lady Gaga'),
(23, 'Kagura'),
(24, 'Cougar'),
(25, 'Lion'),
(27, 'Tiger'),
(28, 'Itachi'),
(29, 'Dinosaur'),
(30, 'Hidan'),
(31, 'Kakuzo'),
(32, 'Alien'),
(33, 'Alien from Mars'),
(34, 'Alien from Neptune'),
(35, 'Alien from Delta Quadrant'),
(36, 'Eliot');

-- --------------------------------------------------------

--
-- Table structure for table `students_courses`
--

CREATE TABLE `students_courses` (
  `id` int(11) NOT NULL,
  `student_id` int(11) NOT NULL,
  `course_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `students_courses`
--

INSERT INTO `students_courses` (`id`, `student_id`, `course_id`) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 2, 2),
(4, 15, 1),
(5, 3, 1),
(6, 16, 1),
(8, 34, 1),
(9, 4, 3),
(10, 11, 3),
(11, 22, 3),
(12, 35, 3),
(13, 12, 3),
(14, 2, 3),
(15, 24, 4),
(16, 6, 4),
(17, 13, 4),
(18, 27, 4),
(19, 33, 4),
(20, 10, 4),
(21, 17, 4),
(22, 34, 5),
(23, 29, 5),
(24, 31, 5),
(25, 14, 5),
(26, 19, 5),
(28, 36, 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `students_courses`
--
ALTER TABLE `students_courses`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;
--
-- AUTO_INCREMENT for table `students_courses`
--
ALTER TABLE `students_courses`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
