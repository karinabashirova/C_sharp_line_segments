using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace test_task_c_sharp
{

    public partial class Form1 : Form
    {
        bool light_mode = true;  // режим подсветки, по умолчанию включен
        
        public Form1()
        {
            InitializeComponent();
        }        

        lines_calculation lines = new lines_calculation();
        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e) // записываем координаты кликов мышкой
        {
            lines.points.Add(new PointF(e.X, e.Y));  // храним координаты кликов мышкой
            if (lines.points.Count > 1)  // если количество точек достаточно, отрисовываем линии
            {
                lines.start_point = lines.points.Count - 1;
                lines.checking_highlighting();  // проверяем, подсвечивать ли отрезок
                this.Refresh();  // обновляем отрисовку формы
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen pen_rect = new Pen(Color.Blue, 2);// цвет ограничивающего прямоугольника
            Pen pen_normal_line = new Pen(Color.Black, 1);// цвет обычной линии и ее ширина
            Pen pen_highlighted_line = new Pen(Color.Red, 3);// цвет подсвеченной линии и ее ширина
            int side_size = 35;  // длина и ширина боковых отступов от ограничивающего прямоугольника
            int field_size = 400;  // длина и ширина граничивающего прямоугольника
            int side_field = side_size + field_size; // длина одной боковой части + длина прямоугольника
            gr.DrawRectangle(pen_rect, side_size, side_size, field_size, field_size);  // нарисовали ограничивающий прямоугольник
            PointF[] points_array = lines.points.ToArray();

            if (points_array.Length > 1)  //если кликнуто как минимум 2 точки, соединяем их
            {
                if (light_mode == true)  // если включен режим подсветки
                {
                    gr.DrawLines(pen_normal_line, points_array);  // нарисовали все обычные линии
                    for (int i = 1; i < lines.points.Count; i++)
                    {
                        if (lines.check_lights[i-1] == true)
                            gr.DrawLine(pen_highlighted_line, lines.points[i - 1], lines.points[i]);
                    }
                }
                else // если режим без подсветки, нарисовали обычные линии
                    gr.DrawLines(pen_normal_line, points_array);
            }
            gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }

        private void highlight_off_Click(object sender, EventArgs e)  // кнопка выключения режима подсветки
        {
            light_mode = false;
            this.Refresh();
            highlight_on.Enabled = true;  // включили кнопку включения подсветки
            highlight_off.Enabled = false;  // выключили кнопку выключения подсветки
        }

        private void highlight_on_Click(object sender, EventArgs e)  // кнопка включения режима подсветки
        {
            light_mode = true;
            this.Refresh();
            highlight_off.Enabled = true;  // включили кнопку выключения подсветки
            highlight_on.Enabled = false;  // выключили кнопку включения подсветки
        }

        private void draw_btn_Click(object sender, EventArgs e)  // рисуем отрезки вручную, старые сбрасываются
        {
            lines.points.Clear();  // сбросили старые значения координат
            lines.check_lights.Clear();
            this.Refresh();  // обновили форму
        }

        private void rnd_btn_Click(object sender, EventArgs e)  // рисуем рандомные отрезки
        {
            lines.points.Clear();  // сбросили старые значения координат
            lines.check_lights.Clear();
            lines.rnd_lines();  // заполнение списка рандомными координатами отрезков
            lines.start_point = 1;
            lines.checking_highlighting();
            this.Refresh();  // обновили форму
        }

        private void save_txt_Click(object sender, EventArgs e)  // сохранили массив в файл
        {
            if (lines.points.Count > 1)
            {
                StreamWriter str = new StreamWriter("coord.txt");
                string coord;
                for (int i = 0; i < lines.points.Count - 1; i++)
                {
                    coord = lines.points[i].X.ToString() + " " + lines.points[i].Y.ToString() + " " + lines.check_lights[i];
                    str.WriteLine(coord);
                }
                coord = lines.points[lines.points.Count - 1].X.ToString() + " " + lines.points[lines.points.Count - 1].Y.ToString();
                str.WriteLine(coord);

                str.Close();
            }
        }

        private void open_txt_Click(object sender, EventArgs e)  // считали массив из файла
        {
            lines.points.Clear();
            lines.check_lights.Clear();
            StreamReader sr = new StreamReader("coord.txt");
            string line;
            int x, y;
            bool c;
            while ((line = sr.ReadLine()) != null)
            {
                string[] coord = line.Split(new char[] { ' ' });
                x = Convert.ToInt32(coord[0]);
                y = Convert.ToInt32(coord[1]);
                lines.points.Add(new PointF(x, y));
                if (coord.Length == 3)
                {
                    c = Convert.ToBoolean(coord[2]);
                    lines.check_lights.Add(c);
                }
                Console.WriteLine(line);
            }
            sr.Close();
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            highlight_on.Enabled = false;  // при загрузке по умолчанию подсветка включена, кнопка включения заблокирована
        }
    }

    
    public partial class lines_calculation
    {
        public int lines_count;  // число линий для рандомного заполнения
        public int full_size; // полная длина/ширина поля отрисовки
        public List<PointF> points = new List<PointF> { };  // список для хранения коррдинат кликов мышкой (будущие линии)
        public List<bool> check_lights = new List<bool> { };  // список для хранения показателей, нужно ли подсвечивать линию
        public int start_point;  // стартовая точка, с которой продолжаем отрисовку
        public lines_calculation() { full_size = 470; lines_count = 15; }

        public void rnd_lines()  // функция заполнения списка случайными координатами
        {
            Random rnd = new Random();
            for (int i = 0; i < lines_count - 1; i++) // задали рандомные значения для координат
            {
                int x = rnd.Next(0, full_size);
                int y = rnd.Next(0, full_size);
                points.Add(new PointF(x, y));
            }
            points.Add(new PointF(points[0].X, points[0].Y)); // замкнули линию
        }

        public void checking_highlighting()  // функция проверки, нужно ли подсвечивать линию
        {
            double tan_alpha_prev, tan_alpha_new, tan_beta_prev, tan_beta_new;
            int side_size = 35;  // длина и ширина боковых отступов от ограничивающего прямоугольника
            int field_size = 400;  // длина и ширина граничивающего прямоугольника
            int side_field = side_size + field_size; // длина одной боковой части + длина прямоугольника
            if (points.Count > 1)  //если кликнуто как минимум 2 точки, соединяем их
            {
                for (int i = start_point; i < points.Count; i++)
                {
                    // если одна точка из двух соедняемых прямой лежит внутри огр. прямоугольника, подсвечиваем прямую
                    if ((points[i - 1].X >= side_size && points[i - 1].Y >= side_size && points[i - 1].X <= side_field && points[i - 1].Y <= side_field) ||
                    (points[i].X >= side_size && points[i].Y >= side_size && points[i].X <= side_field && points[i].Y <= side_field))
                        check_lights.Add(true);

                    // если линии идут крестом через квадрат (из угла в угол)
                    else if ((points[i - 1].X < side_size && points[i - 1].Y < side_size && points[i].X > side_field && points[i].Y > side_field)) // из лев. верх. в прав. нижн
                        check_lights.Add(true);
                    else if (points[i - 1].X > side_field && points[i - 1].Y < side_size && points[i].X < side_size && points[i].Y > side_field)  //  из прав. верх. в лев. нижн.
                        check_lights.Add(true);
                    else if (points[i - 1].X > side_field && points[i - 1].Y > side_field && points[i].X < side_size && points[i].Y < side_size)  // из прав. ниж. в лев. верх
                        check_lights.Add(true);
                    else if (points[i - 1].X < side_size && points[i - 1].Y > side_field && points[i].X > side_field && points[i].Y < side_size)  // из лев. ниж. в прав. верх.
                        check_lights.Add(true);

                    // иначе проверяем, проходит ли прямая через прямоугольник
                    else if (points[i - 1].X < side_size && points[i - 1].Y > side_size && points[i - 1].Y < side_field && points[i].X > side_size)  // левая сторона
                    {
                        //посчитали тангенсы угла наклона прямой, если лежат в нужных пределах, подсвечиваем
                        if (points[i - 1].Y > points[i].Y) // если старая точка лежит ниже новой
                        {
                            tan_alpha_prev = (points[i - 1].Y - side_size) / (side_size - points[i - 1].X);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и верхний угол прямоугольника
                            tan_alpha_new = (points[i - 1].Y - points[i].Y) / (points[i].X - points[i - 1].X);  // тангенс угла наклона отрисовываемой прямой
                            if ((tan_alpha_new) < (tan_alpha_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                        else // если старая точка лежит выше новой
                        {
                            tan_beta_prev = (side_field - points[i - 1].Y) / (side_size - points[i - 1].X);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и нижний угол прямоугольника
                            tan_beta_new = (points[i].Y - points[i - 1].Y) / (points[i].X - points[i - 1].X);  // тангенс угла наклона отрисовываемой прямой
                            if ((tan_beta_new) < (tan_beta_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                    }
                    else if (points[i - 1].X > side_field && points[i - 1].Y > side_size && points[i - 1].Y < side_field && points[i].X < side_field)  // правая сторона
                    {
                        //посчитали тангенсы угла наклона прямой, если лежат в нужных пределах, подсвечиваем
                        if (points[i - 1].Y > points[i].Y) // если старая точка лежит ниже новой
                        {
                            tan_alpha_prev = (points[i - 1].Y - side_size) / (points[i - 1].X - side_field);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и верхний угол прямоугольника
                            tan_alpha_new = (points[i - 1].Y - points[i].Y) / (points[i - 1].X - points[i].X);  // тангенс угла наклона отрисовываемой прямой
                            if ((tan_alpha_new) < (tan_alpha_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                        else // если старая точка лежит выше новой
                        {
                            tan_beta_prev = (side_field - points[i - 1].Y) / (points[i - 1].X - side_field);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и нижний угол прямоугольника
                            tan_beta_new = (points[i].Y - points[i - 1].Y) / (points[i - 1].X - points[i].X);  // тангенс угла наклона отрисовываемой прямой
                            if ((tan_beta_new) < (tan_beta_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                    }
                    else if (points[i - 1].Y < side_size && points[i - 1].X > side_size && points[i - 1].X < side_field && points[i].Y > side_size)  // верхняя сторона
                    {
                        //посчитали тангенсы угла наклона прямой, если лежат в нужных пределах, подсвечиваем
                        if (points[i - 1].X > points[i].X) // новая точка лежит левее старой
                        {
                            tan_alpha_prev = (points[i - 1].X - side_size) / (side_size - points[i - 1].Y);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и левый угол прямоугольника
                            tan_alpha_new = (points[i - 1].X - points[i].X) / (points[i].Y - points[i - 1].Y);  // тангенс угла наклона отрисовываемой прямой
                            if ((tan_alpha_new) < (tan_alpha_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                        else // новая точка лежит правее старой
                        {
                            tan_beta_prev = (side_field - points[i - 1].X) / (side_size - points[i - 1].Y);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и правый угол прямоугольника
                            tan_beta_new = (points[i].X - points[i - 1].X) / (points[i].Y - points[i - 1].Y);  // тангенс угла наклона отрисовываемой прямой

                            if ((tan_beta_new) < (tan_beta_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                    }
                    else if (points[i - 1].Y > side_field && points[i - 1].X > side_size && points[i - 1].X < side_field && points[i].Y < side_field)  // нижняя сторона
                    {
                        //посчитали тангенсы угла наклона прямой, если лежат в нужных пределах, подсвечиваем
                        if (points[i - 1].X > points[i].X) // новая точка лежит левее старой
                        {
                            tan_alpha_prev = (points[i - 1].X - side_size) / (points[i - 1].Y - side_field);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и левый угол прямоугольника
                            tan_alpha_new = (points[i - 1].X - points[i].X) / (points[i - 1].Y - points[i].Y);  // тангенс угла наклона отрисовываемой прямой
                            if ((tan_alpha_new) < (tan_alpha_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                        else // новая точка лежит правее старой
                        {
                            tan_beta_prev = (side_field - points[i - 1].X) / (points[i - 1].Y - side_field);  // тангенс угла наклона прямой, проходящей через начальную точку прямой и правый угол прямоугольника
                            tan_beta_new = (points[i].X - points[i - 1].X) / (points[i - 1].Y - points[i].Y);  // тангенс угла наклона отрисовываемой прямой
                            if ((tan_beta_new) < (tan_beta_prev))
                                check_lights.Add(true);
                            else
                                check_lights.Add(false);
                        }
                    }
                    else if (points[i - 1].X < side_size && points[i - 1].Y > side_field && points[i].X > side_size && points[i].Y < side_field)  // из нижнего левого кввадрата
                        check_lights.Add(true);
                    else if (points[i - 1].X < side_size && points[i - 1].Y < side_size && points[i].X > side_size && points[i].Y > side_size)  // из верхнего левого кввадрата
                        check_lights.Add(true);
                    else if (points[i - 1].X > side_field && points[i - 1].Y > side_field && points[i].X < side_field && points[i].Y < side_field)  // из нижнего правого кввадрата
                        check_lights.Add(true);
                    else if (points[i - 1].X > side_field && points[i - 1].Y < side_size && points[i].X < side_field && points[i].Y > side_size)  // из верхнего правого квадрата
                        check_lights.Add(true);

                    else
                        check_lights.Add(false);  // иначе не подсвечиваем отрезок
                }

            }

        }
    }

}
