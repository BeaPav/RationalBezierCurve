   M S B A M L   `   `   `  ����I  ERacionalBezier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:RacionalBezierRacionalBezier  5   	       RacionalBezier.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:RacionalBezier     5   	     � Title$  
MainWindow��5   	   $	��450��6   $��859.2��6)   ���	#FFA3A3A3.��5	      � -   5         �Name$ Ellipse��5	      .+�5
   
   �� -   5         ��Name$ g��5
      $
��Left=�6   $	��400��67   $��	10,10,0,0q�6D   $	��Top=�6W   $	��650��6o   
�������6�   	� 5   
   -   5         	�Name$ GeneralCurve��5      $��General curve��6,   $
��Left=�6D   $��
665,10,0,0q�6_   $	��Top=�6s    d� 	IsChecked$
 TrueY�6�   6�   �� 5   
   -   5         ��Name$ WeightsTextBox��5      $
��Left=�6*   $��43��6E   $��
665,56,0,0q�6Q    �� TextWrapping$
 Wrap=�6e   $	��Top=�6y   $	��178��6�   6�   	� 5   
   -   5        $��Ellipse��5      $
��Left=�6(   $��665,108,0,0q�6C   $	��Top=�6X   6p   	� 5   
   -   5        $ Incircle��5      $��Incircle��6(   $
��Left=�6;   $��665,128,0,0q�6V   $	��Top=�6k   6�   �� 5   
   -   5         ��Name$ ParamCheckBox��5      $��Visualize parameter��6*   $
��Left=�6H   $��665,318,0,0q�6c   $	��Top=�6x   6�   �� 5   
   -   5         ��Name$ Slider��5      $
��Left=�6!   $��666,338,0,0q�6<   $	��Top=�6Q   $	��122��6i   	 � LargeChange$		 0.1Q�6u   
 � Maximum$
 1Q�6�    � SmallChange$
 0.01Q�6�    �� TickFrequency$
 0.01Q�6�    �� IsSnapToTickEnabled .  �� AutoToolTipPlacement$ BottomRight=�6�    �� AutoToolTipPrecision$ 2��6�   6  �� 5   
   -	   5        $ FlowCheckBox��5      $��Parametrization flow��6)   $
��Left=�6H   $��665,385,0,0q�6c   $	��Top=�6x   $��16��6�   �� 5   
   -
   5         ��Name$ DelBut��5      $��Delete��6!   $
��Left=�62   $��668,294,0,0q�6M   $	��Top=�6b   $	��175��6z   6�   �� 5   
   $��weights (integers): ��6   $
��Left=�6/   $��
672,25,0,0q�6J   $	��Top=�6^   $��26��6v   $	��124��6�   �� 5   
   -   5         ��Name$ Control��5      $�� ��6!   $
��Left=�6,   $	��133��6G   $��665,148,0,0q�6T   $	��Top=�6i   $	��178��6�   5      5      