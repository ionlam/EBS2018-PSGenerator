EBS2018-PSGenerator
Event Based Systems - Publications & Subscriptions Generator
ion.lamasanu@info.uaic.ro
MSD2@infoiasi

---

Publication schema and publication generator settings are present in file named publication.config
Subscription schema and subscription generator settings are present in file named subscription.config

Publications are generated in file named publications.txt
Subscriptions are generated in file named subscriptions.txt

---

https://profs.info.uaic.ro/~eonica/ebs/eval.html

Tema practica individuala - termen 30 martie:

Scrieti un program care sa genereze aleator seturi echilibrate de subscriptii si publicatii cu posibilitatea de fixare a: numarului total de mesaje, ponderii pe frecventa campurilor din subscriptii si ponderii operatorilor de egalitate din subscriptii pentru campul cu domeniul cel mai mic. Publicatiile vor avea o structura fixa de campuri la alegere.
Exemplu: 
Publicatie: {(company,"Google");(value,90.0);(drop,10.0);(variation,0.73);(date,2.02.2022)} - Structura fixa a campurilor publicatiei e: company-string, value-double, drop-double, variation-double, date-data; pentru anumite campuri (company, date), se pot folosi seturi de valori prestabilite de unde se va alege una la intamplare; pentru alte campuri (drop, variation, date) se pot stabili limite inferioare si superioare intre care se va alege una la intamplare. 

Subscriptie:{(company,=,"Google");(value,>=,90);(variation,<,0.8)} - Unele campuri pot lipsi; frecventa campurilor prezente trebuie sa fie configurabila (ex. 90% company - macar 90% din subscriptiile generate trebuie sa includa campul "company"); pentru campul cu domeniul cel mai mic (ex. company e definit ca avand valori intr-o multime de doar 5 companii) se poate configura un minim de frecventa pentru operatorul "=" (ex. macar 90% din subscriptiile generate sa aiba ca operator pe acest camp egalitatea). 

Setul generat va fi memorat in fisiere text.
