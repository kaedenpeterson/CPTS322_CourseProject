# Sprint 1 Cleanup

## Overview
The navigation system implemented in the sprint 1 cleanup is designed to manage view transitions and dynamically update the active view (CurrView) based on user interaction with the menu bar. The system is built around the INavigationService interface, which allows for switching between different views, such as AdminView and CoursesView. The MainView acts as a container for these dynamic views, maintaining a reference to the current active view. When a menu item is selected, the CurrView property of the NavigationService is updated, which triggers the view change. This update is reflected in the user interface through data binding. For instance, when an admin selects a menu option, the system updates CurrView to display the corresponding view, such as the courses page. The AdminRootViewModel monitors changes to the CurrView property and switches the displayed content based on these changes.

## New directory
**CoreUI:** Holds core UI components such as navigation, root views, and overall structure of switching between views.

## New components
**Course:** Represents a course in the system. Stores course details.  

**Schedule:** Represents the schedule of a course (days of week, class time, start/end date). Includes formatted representations of the schedule for display purposes.  

**AdminRootView:** ViewModel for the admin root view, responsible for handling navigation through a menu bar. Also displays the sub view (CurrView) as a ContentControl.  

**StudentRootView:** ViewModel for the student root view, responsible for handling navigation through a menu bar. Also displays the sub view (CurrView) as a ContentControl.  

**PopupWindow:** Defines a template that is used for creating popup windows.  





