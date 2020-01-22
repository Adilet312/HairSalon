DROP DATABASE IF EXISTS adilet_momunaliev;
CREATE DATABASE adilet_momunaliev;
USE adilet_momunaliev;
/*Table name stylist and it has three fields such as: stylist's id , name ,and specialty. */
CREATE TABLE adilet_momunaliev.Stylists(
                    StylistId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
                    StylistName VARCHAR(255) NOT NULL,
                    StylistSpecialty VARCHAR(255) NOT NULL
);
CREATE TABLE adilet_momunaiev.`Clients`(
					ClientId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
                    StylistId INT NOT NULL,
                    ClientName VARCHAR(255) NOT NULL,
                    CONSTRAINT fk_stylist_stylistId FOREIGN KEY(StylistId)
                    REFERENCES Stylists(StylistId) 
                    ON DELETE CASCADE
);
