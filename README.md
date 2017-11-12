Simple solution for Byteland unification problem using C#.

Problem Description

Byteland is a strange country, with many cities, but with a poorly developed road network (in fact, there is exactly one route from each city to any other city, possibly leading through other cities). Until recently, the cities of Byteland were independently governed by proud Mayors, who chose not to integrate too tightly with their neighbours. However, recent opinion polls among Bytelandian computer programmers have shown a number of disturbing trends, including a sudden drop in pizza consumption. Since this had never before happened in Byteland and seemed quite inexplicable, the Mayors sought guidance of the High Council of Wise Men of Byteland. After a long period of deliberation, the Council ruled that the situation was very serious indeed: the economy was in for a long-term depression! Moreover, they claimed that tighter integration was the only way for the Bytelandian cities to survive. Whether they like it or not, the Mayors must now find a way to unite their cities as quickly as possible. However, this is not as easy as it sounds, as there are a number of important constraints which need to be fulfilled:

•	Initially, each city is an independent State. The process of integration is divided into steps.
  
•	At each step, due to the limited number of diplomatic envoys available, a State can only be involved in a unification process with at       most one other state. At each step two States can unite to form a new State, but only if there exists a road directly connecting some       two cities of the uniting States.
  
•	The unification process is considered to be complete when all the cities belong to the same, global State.

Input

Input parameters are embedded in the code. An array is defined that defines the link between cities. We assume that the cities are numbered 0,...,k-1. The array contains exactly k-1 integers, and the i-th integer having a value of p represents a road connecting cities having numbers i+1 and p in Byteland.

Output

Minimum step count to link all city.

Example

Input:  0 1 2 0 0 3 3       => roads between cities

Output: 5                   => minimum step to link all city
