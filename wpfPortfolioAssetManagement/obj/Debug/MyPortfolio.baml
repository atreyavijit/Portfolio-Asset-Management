   M S B A M L   `   `   `  ����V  RwpfPortfolioAssetManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:wpfPortfolioAssetManagementwpfPortfolioAssetManagement  5   	   -    'wpfPortfolioAssetManagement.MyPortfolio   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:wpfPortfolioAssetManagement     5   	   -   5          � Title$  My Portfolio��5	   	   $	��246��6   $	��664��6+   .��5
      � .+�5   
   �� -   5         ��Name$ 	lblSymbol��5      $��Symbol: ��6#   $��30��66   $	��Top=�6B   $��210,10,296,0q�6Z   �� 5   
   -   5         ��Name$ lvWatchlist��5      $
��Left=�6(   $	��195��6C   $��	10,10,0,0q�6P   $	��Top=�6c   $	��195��6{   6�   �� 5   
   -   5        $ lblNumOfShares��5      $��Num of shares owned: ��6(   $��30��6H   $	��Top=�6T   $��210,45,296,0q�6l   �� 5   
   -   5        $ lblPurchase��5      $��Unit Purchase Price: ��6%   $��30��6E   $	��Top=�6Q   $��210,75,296,0q�6i   �� 5   
   -   5        $ lblPurchaseCurrent��5      $��Current Market Price: ��6,   $��30��6M   $	��Top=�6Y   $��210,135,296,0q�6q   �� 5   
   -   5        $ lblSell��5      $��Unit Sell Price: ��6!   $��30��6=   $	��Top=�6I   $��210,104,296,0q�6a   �� 5   
   -   5         ��Name$ btnBuy��5      $��Buy Now��6!   $
��Left=�63   $��25��6N   $��210,180,0,0q�6Z   $	��Top=�6o   $	��110��6�   6�   �� 5   
   -	   5        $ btnSell��5      $��Sell Now��6"   $
��Left=�65   $��25��6P   $��335,180,0,0q�6\   $	��Top=�6q   $	��110��6�   �� 5   
   -
   5        $ lvPortfolio��5      $
��Left=�6(   $	��195��6C   $��
451,11,0,0q�6P   $	��Top=�6d   $	��195��6|   6�   �� 5   
   -   5         ��Name$ tbSymbol��5      $
��Left=�6$   $��20��6?   $��
355,12,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $��85��6�    �� TextAlignment$ Center=�6�   �� 5   
   -   5        $ tbNumOfShares��5      $
��Left=�6)   $��20��6D   $��
355,47,0,0q�6P   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $��85��6�   $ Center=�6�   �� 5   
   -   5        $ tbPurchasePrice��5      $
��Left=�6+   $��20��6F   $��
355,77,0,0q�6R   $
 Wrap=�6f   $�� ��6z   $	��Top=�6�   $��85��6�   $ Center=�6�   �� 5   
   -   5        $ tbSellPrice��5      $
��Left=�6'   $��20��6B   $��355,106,0,0q�6N   $
 Wrap=�6c   $�� ��6w   $	��Top=�6   $��85��6�   $ Center=�6�   �� 5   
   -   5        $ tbMarketPrice��5      $
��Left=�6)   $��20��6D   $��355,137,0,0q�6P   $
 Wrap=�6e   $�� ��6y   $	��Top=�6�   $��85��6�   $ Center=�6�   5      5      