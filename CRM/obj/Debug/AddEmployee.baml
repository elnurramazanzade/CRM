   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   > :CRM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CRMCRM 5   	      CRM.AddEmployee   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   #localclr-namespace:CRM     5   	     � Title$  AddEmployee��5	   	    � WindowStartupLocation$ CenterScreen=�6   $	��468��6B   $	��270��6O    � 
ResizeMode$ NoResize=�6[   .��5
      � 
������.+�5   
   �� $��Adı:��6   $
��Left=�6   $��	30,10,0,0q�6:   $	��Top=�6M   $��18(�6e   �� 5   
   -   5         ��Name$ TxtEmployeeName��5      $��25��6+   $��	30,45,0,0q�67    �� TextWrapping$
 Wrap=�6J   $	��Top=�6^   $
��Left=�6v   $	��200��6�   $��18(�6�   
����� �$��Center=�6�   �� 5   
   $��Soyadı:��6   $
��Left=�6"   $��	30,70,0,0q�6=   $	��Top=�6P   $��18(�6h   �� 5   
   -   5        $ TxtEmployeeSurname��5      $��25��6.   $��
30,105,0,0q�6:   $
 Wrap=�6N   $	��Top=�6b   $
��Left=�6z   $	��200��6�   $��18(�6�   
����� �$��Center=�6�   �� 5   
   $��Vəzifəsi:��6   $
��Left=�6$   $��
30,130,0,0q�6?   $	��Top=�6S   $��18(�6k   �� 5   
   -   5         ��Name$ CmbPosition��5      $
��Left=�6(   $��
30,165,0,0q�6C   $	��Top=�6W   $	��200��6o   $��18(�6{   
���   �$��Center=�6�   �� 5   
   $��Telefon nömrəsi:��6   $
��Left=�6+   $��
30,200,0,0q�6F   $	��Top=�6Z   $��18(�6r   �� 5   
   -   5        $ TxtPhone��5      $��25��6$   $��
30,235,0,0q�60   $
 Wrap=�6D   $	��Top=�6X   $
��Left=�6p   $	��200��6�   $��18(�6�   
����� �$��Right=�6�   $��Center=�6  �� 5   
   $��İşə qəbul tarixi:��6   $
��Left=�6,   $��
30,260,0,0q�6G   $	��Top=�6[   $��18(�6s   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DpRecruitmentDate��5      
 �� Now   SelectedDate# Z 60   $��
30,295,0,0q�6[   $	��Top=�6o   $��16(�6�   $��Center=�6�   
���   �$
��Left=�6�   $	��200��6�   �� 5   
   -   5        	 ��Name$	 TxtBlcAttention��5      $��30,330,30,0q�6-   $
��Wrap=�6B   $	��Top=�6V   $��14(�6n   
���  ��$��33��6�   �� 5   
   -   5        
 ��Name$
 BtnSave��5      $��Yadda saxla��6"   $
��Left=�68   $��
30,370,0,0q�6S   $	��Top=�6g   $��95��6   $��35��6�   $��16(�6�    � Cursor$
 Hand��6�   6�   �� 5   
   -   5        $
 BtnClear��5      $��	Təmizlə��6#   $
��Left=�65   $��135,370,0,0q�6P   $	��Top=�6e   $��95��6}   $��35��6�   $��16(�6�   $
 Hand��6�   6�   5      5      