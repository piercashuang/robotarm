#A,0,0,1.57,0,0,0,4
#模式关节 关节角度 最后一位姿态选择
import math
from math import pi
from math import cos as cos
from math import sin as sin
from math import atan2 as atan2
from math import acos as acos
from math import asin as asin
from math import sqrt as sqrt
from math import pi as pi
from numpy import linalg
import numpy as np
import sys
import socket
import threading
global_variablex = None
global_variabley = None
global_variablez = None
jointa=None
jointb=None
jointc=None
jointd=None
jointe=None
jointf=None
poscase=None
def start_server():
    global global_variablex, global_variabley, global_variablez,poscase
    # 创建一个TCP/IP套接字
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # 绑定套接字到特定的地址和端口
    server_address = ('', 6069)
    server_socket.bind(server_address)

    # 开始监听连接
    server_socket.listen(1)
    print('服务器启动，等待连接...')

    while True:
        # 等待连接
        client_socket, client_address = server_socket.accept()
        print(f'接受来自 {client_address} 的连接')

        try:
            # 接收数据
            data = client_socket.recv(1024)
            print(f'收到数据: {data.decode("utf-8")}')
            data = data.decode("utf-8");
            if data[0] == 'A':
                print("开始解算");
                # 创建六自由度机械臂类型的对象arm
                arm = SixDofArm()

                theta = data.split(',');
                data = theta[:-1]
                poscase = theta[-1]
                # 将列表中的每个字符串转换为相应的数字，去掉所有字母
                converted_data = []
                for value in theta:
                    try:
                        # 尝试将字符串转换为浮点数
                        num_value = float(value)
                        converted_data.append(num_value)
                    except ValueError:
                        print(f"Warning: Unable to convert '{value}' to a number.")

                print(converted_data)
                
                # theta = [0,0,-pi/2,0,0,0]
                #theta = [0,0,pi/2,0,0,0]
                arm.forward_kinematics(converted_data)
                file_path = "D:/communication.txt"

                try:
                    with open(file_path, 'r') as file:
                        content = file.read()
                        print("File Content:")
                        print(content)
                except FileNotFoundError:
                    print(f"The file '{file_path}' was not found.")
                except Exception as e:
                    print(f"An error occurred: {e}")


                file_path = "D:/communication.txt"

                try:
                    # 使用 'w' 模式打开文件，如果文件不存在将会创建一个新文件
                    with open(file_path, 'w') as file:
                        # 写入数据
                        print(global_variablex)
                        print(global_variabley)
                        print(global_variablez)
                        file.write(str(global_variablex) + '\n')
                        file.write(str(global_variabley) + '\n')
                        file.write(str(global_variablez) + '\n')
                        
                        
                        print(type(str(jointa)),jointb,jointc,jointd,jointe,jointf)
                        file.write(str(jointa).strip("[]") + '\n')
                        file.write(str(jointb).strip("[]") + '\n')
                        file.write(str(jointc).strip("[]") + '\n')
                        file.write(str(jointd).strip("[]") + '\n')
                        file.write(str(jointe).strip("[]") + '\n')
                        file.write(str(jointf).strip("[]") + '\n')
                        
                    print("Data has been written to the file.")
                except Exception as e:
                    print(f"An error occurred: {e}")
                    
            # 发送响应数据
            response = 'Hello, client!'
            client_socket.sendall(response.encode('utf-8'))

        finally:
            # 关闭连接
            print(f'关闭与 {client_address} 的连接')
            client_socket.close()



class SixDofArm():
    # 构造方法，初始化机械臂所用的所有电机
    def __init__(self):
        super(SixDofArm, self).__init__()


    # 机械臂复位函数
    def home(self):
        for i in range(6):
            self.motors[i].set_pos(0)
        self.hand.set_pos(0)

    # def set_hand(self, pos):
        # self.hand.set_pos(pos)

    # 机械臂示例位置函数
    def demo_pose(self):
        self.motors[0].set_pos(-0.6)
        self.motors[1].set_pos(0.5)
        self.motors[2].set_pos(1)
        self.motors[3].set_pos(-0.9)
        self.motors[4].set_pos(1.57)
        self.motors[5].set_pos(3.14)
        # self.hand.set_pos(0.5)


    # 正运动学
    def dhtransform(self,alpha, a, d, theta):
        matrixs = np.matrix(np.zeros((4, 4)))

        sth = math.sin(theta)
        cth = math.cos(theta)
        sa = math.sin(alpha)
        ca = math.cos(alpha)

        matrixs[0, 0] = cth
        matrixs[1, 0] = sth
        matrixs[0, 1] = -sth * ca
        matrixs[1, 1] = cth * ca
        matrixs[2, 1] = sa
        matrixs[0, 2] = sth * sa
        matrixs[1, 2] = -cth * sa
        matrixs[2, 2] = ca
        matrixs[0, 3] = a * cth
        matrixs[1, 3] = a * sth
        matrixs[2, 3] = d
        matrixs[3, 3] = 1

        return matrixs


    def forward_kinematics(self, theta):
        global global_variablex, global_variabley, global_variablez
        d1 =  150
        a2 =  -145
        a3 =  -145
        d4 =  75
        d5 =  75
        d6 =  70
        T01 = self.dhtransform(pi/2 , 0 , d1, theta[0])
        T12 = self.dhtransform(0    , a2, 0 , theta[1]-pi/2)
        T23 = self.dhtransform(0    , a3, 0 , theta[2])
        T34 = self.dhtransform(pi/2 , 0 , d4, theta[3]-pi/2)
        T45 = self.dhtransform(-pi/2, 0 , d5, theta[4])
        T56 = self.dhtransform(0    , 0 , d6, theta[5])

        T06 = T01*T12*T23*T34*T45*T56

        print(T06.round(2))
        print("x: ",T06[0,3].round(2))
        print("y: ",T06[1,3].round(2))
        print("z: ",T06[2,3].round(2))
        global_variablex = T06[0,3].round(2)
        global_variabley = T06[1,3].round(2)
        global_variablez = T06[2,3].round(2)

        self.inverse_kinematics(T06)
        # for i in range(6):
            # self.motors[i].set_pos(theta[i])

        # return T06



    # # 逆运动学算法
    def inverse_kinematics(self,T06):
        global jointa, jointb, jointc,jointd,jointe,jointf,poscase
#         选择解的一种
        print(type(poscase))
        print(poscase)
        pos_arr = invKine(T06)[:,int(poscase)]
        pos_arr[1] = pi/2.0 + pos_arr[1,0]
        pos_arr[2] = -pos_arr[2,0]
        pos_arr[3] = pi/2.0 + pos_arr[3,0]


        print(pos_arr.round(2))
        joint=pos_arr.round(2)
        jointa=joint[0]
        jointb=joint[1]
        jointc=joint[2]
        jointd=joint[3]
        jointe=joint[4]
        jointf=joint[5]
        print(str(jointa))
#         for i in range(6):
#             self.motors[i].set_pos(pos_arr[i,0])


## UR5/UR10 Inverse Kinematics - Ryan Keating Johns Hopkins University

# ***** lib
import numpy as np
from numpy import linalg


import cmath
import math
from math import cos as cos
from math import sin as sin
from math import atan2 as atan2
from math import acos as acos
from math import asin as asin
from math import sqrt as sqrt
from math import pi as pi

global mat
mat=np.matrix


# ****** Coefficients ******


global d1, a2, a3, a7, d4, d5, d6
# d1 =  0.15
# a2 =  -0.145
# a3 =  -0.145
# d4 =  0.075
# d5 =  0.075
# d6 =  0.07

# d1 =  222.1
# a2 =  269.546
# a3 =  80
# d4 =  147
# d5 =  46.5
# d6 =  59.9
d1 =  150
a2 =  -145
a3 =  -145
d4 =  75
d5 =  75
d6 =  70

global d, a, alph

d = mat([d1, 0, 0, d4, d5, d6])
a = mat([0 ,a2 ,a3 ,0 ,0 ,0])
alph = mat([pi/2, 0, 0, pi/2, -pi/2, 0 ]) # ur10


# ************************************************** FORWARD KINEMATICS

def AH( n,th,c  ):

  T_a = mat(np.identity(4), copy=False)
  T_a[0,3] = a[0,n-1]
  T_d = mat(np.identity(4), copy=False)
  T_d[2,3] = d[0,n-1]

  Rzt = mat([[cos(th[n-1,c]), -sin(th[n-1,c]), 0 ,0],
	         [sin(th[n-1,c]),  cos(th[n-1,c]), 0, 0],
	         [0,               0,              1, 0],
	         [0,               0,              0, 1]],copy=False)


  Rxa = mat([[1, 0,                 0,                  0],
			 [0, cos(alph[0,n-1]), -sin(alph[0,n-1]),   0],
			 [0, sin(alph[0,n-1]),  cos(alph[0,n-1]),   0],
			 [0, 0,                 0,                  1]],copy=False)

  A_i = T_d * Rzt * T_a * Rxa


  return A_i

def HTrans(th,c ):
  A_1=AH( 1,th,c  )
  A_2=AH( 2,th,c  )
  A_3=AH( 3,th,c  )
  A_4=AH( 4,th,c  )
  A_5=AH( 5,th,c  )
  A_6=AH( 6,th,c  )

  T_06=A_1*A_2*A_3*A_4*A_5*A_6

  return T_06

# ************************************************** INVERSE KINEMATICS

def invKine(desired_pos):# T60
  th = mat(np.zeros((6, 8)))
  P_05 = (desired_pos * mat([0,0, -d6, 1]).T-mat([0,0,0,1 ]).T)

  # **** theta1 ****

  psi = atan2(P_05[2-1,0], P_05[1-1,0])
  phi = cmath.acos(d4 /sqrt(P_05[2-1,0]*P_05[2-1,0] + P_05[1-1,0]*P_05[1-1,0])).real
  #The two solutions for theta1 correspond to the shoulder
  #being either left or right
  th[0, 0:4] = pi/2 + psi + phi
  th[0, 4:8] = pi/2 + psi - phi
  th = th.real

  # **** theta5 ****

  cl = [0, 4]# wrist up or down
  for i in range(0,len(cl)):
	      c = cl[i]
	      T_10 = linalg.inv(AH(1,th,c))
	      T_16 = T_10 * desired_pos
	      th[4, c:c+2] = cmath.acos((T_16[2,3]-d4)/d6).real
	      th[4, c+2:c+4] = - cmath.acos((T_16[2,3]-d4)/d6).real

  th = th.real

  # **** theta6 ****
  # theta6 is not well-defined when sin(theta5) = 0 or when T16(1,3), T16(2,3) = 0.

  cl = [0, 2, 4, 6]
  for i in range(0,len(cl)):
	      c = cl[i]
	      T_10 = linalg.inv(AH(1,th,c))
	      T_16 = linalg.inv( T_10 * desired_pos )
	      th[5, c:c+2] = atan2((-T_16[1,2]/sin(th[4, c])),(T_16[0,2]/sin(th[4, c]))) if sin(th[4, c]) else 0

  th = th.real

  # **** theta3 ****
  cl = [0, 2, 4, 6]
  for i in range(0,len(cl)):
	      c = cl[i]
	      T_10 = linalg.inv(AH(1,th,c))
	      T_65 = AH( 6,th,c)
	      T_54 = AH( 5,th,c)
	      T_14 = ( T_10 * desired_pos) * linalg.inv(T_54 * T_65)
	      P_13 = T_14 * mat([0, -d4, 0, 1]).T - mat([0,0,0,1]).T
	      t3 = cmath.acos((linalg.norm(P_13)**2 - a2**2 - a3**2 )/(2 * a2 * a3)).real # norm ?
	      th[2, c] = t3.real
	      th[2, c+1] = -t3.real

  # **** theta2 and theta 4 ****

  cl = [0, 1, 2, 3, 4, 5, 6, 7]
  for i in range(0,len(cl)):
	      c = cl[i]
	      T_10 = linalg.inv(AH( 1,th,c ))
	      T_65 = linalg.inv(AH( 6,th,c))
	      T_54 = linalg.inv(AH( 5,th,c))
	      T_14 = (T_10 * desired_pos) * T_65 * T_54
	      P_13 = T_14 * mat([0, -d4, 0, 1]).T - mat([0,0,0,1]).T

	      # theta 2
	      th[1, c] = -atan2(P_13[1], -P_13[0]) + asin(a3* sin(th[2,c])/linalg.norm(P_13))
	      # theta 4
	      T_32 = linalg.inv(AH( 3,th,c))
	      T_21 = linalg.inv(AH( 2,th,c))
	      T_34 = T_32 * T_21 * T_14
	      th[3, c] = atan2(T_34[1,0], T_34[0,0])
  th = th.real

  return th



if __name__ == '__main__':
    server_thread = threading.Thread(target=start_server)
    server_thread.start()