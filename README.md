# Parkent

#### Описание:
Проект представляет из себя приложение, в котором можно очень быстро оплатить парковку, а так же узнать о наличии или отсутствии свободных мест. В приложении можно сразу, заранее, привязать удобную платежную систему и номера автомобилей. Приложение автоматически определяет где находится пользователь, либо же дает возможность выбрать пользователю где он находится, чтобы найти ближайшие парковки или определить какую именно он хочет оплатить. Главным принципом приложения является простота. Все делается буквально в 3 нажатия и занимает не более 20 секунд. 

#### Цели:
-	Создать парковочную систему
-	Получить опыт работы в команде

#### Задачи:
-	Парковочная система создается как отдельное приложение под Android
-	Разработать уникальный дизайн под приложения
- Разработать удобный интерфейс и автоматизировать все, что возможно
-	Создать создать БД на основе UML-схемы

#### Разработчики:
-	Даниелла Колпакова 
-	Артем Дмитриев 
-	Никита Виноградов 

#### Схема:
![UML scheme](https://github.com/dufftie/parkent/blob/master/source/preview.PNG)



# Документация:

#### Начальная страница:
Сначала при входе в программу пользователь попадает на начальную станицу где он может войти в свою учетную запись, в случаи если у пользователя нет своей учетной запись, то пользователь может её создать нажав на кнопку «Регистрация»
![landing](https://github.com/dufftie/parkent/blob/master/source/documentation/landing.jpg)

#### Регистрация:
Если пользователь нажал на кнопку «Регистрация», то его перекинет на страницу для того чтобы зарегистрироваться
![registration](https://github.com/dufftie/parkent/blob/master/source/documentation/registration.jpg)

#### Логин:
Если у пользователя есть учетная запись, то он пропускает шаг с регистрацией и вводит логин и пароль, если же пользователь только что зарегистрировался, то его автоматическии перекидывает на страницу с логином и вводит нужные данные за него.
![login](https://github.com/dufftie/parkent/blob/master/source/documentation/login.jpg)

#### Нахождение локации:
После авторизации, главной страницей становится страница с проверкой местаположения пользователя. Программа смотрит по координатам, где находится пользователь и сверяет его нахождение с базой данных, для того чтобы узнать тариф.
![location](https://github.com/dufftie/parkent/blob/master/source/documentation/location.jpg)

#### Другая локация:
Если же функция работает некорректно, например определяет не тот адрес или устройство не поддерживает функцию, то кнопка "Да", которая подтверждает местонахождение пользователя становится недоступной. В таком случае пользователь может выбрать один из районов Таллина.
![anotherLoc](https://github.com/dufftie/parkent/blob/master/source/documentation/anotherLoc.jpg)

#### Указывание времени:
После определения того, где пользователь находится, он выбирает время, на которое он оплачивает парковку (от 1 до 168 часов) и подтверждает, либо меняет номер из списка уже имеющихся номеров, либо есть возможность изменить уже поставленный номер, который будет оплачен.
![numTime](https://github.com/dufftie/parkent/blob/master/source/documentation/numTime.jpg)
![NumberList](https://github.com/dufftie/parkent/blob/master/source/documentation/NumberList.jpg)

#### Оплата:
После определения тарифа и времени оплаты пользователю выставляется счет. Он может оплатить счет с баланса аккаунта, либо с кредитной карты, в случае, если счет больше, чем баланс, то способы платежа можно совместить.
![payPage](https://github.com/dufftie/parkent/blob/master/source/documentation/payPage.jpg)

#### Меню:
В меню пользователь может:
-Посмотреть оплаченную парковку
-Пополнить и посмотреть баланс
-Сменить пароль
-Изменить настройки аккаунта
![menu](https://github.com/dufftie/parkent/blob/master/source/documentation/menu.jpg)

#### Конечная страница:
После оплаты счета пользователь видит страницу, на которой выводится сообщение об успешной операции.
![celebration](https://github.com/dufftie/parkent/blob/master/source/documentation/celebration.jpg)
