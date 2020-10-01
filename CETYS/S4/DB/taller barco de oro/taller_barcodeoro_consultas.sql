USE taller_barcodeoro;

# 1) Mostrar información del cliente con mayor frecuencia en reparaciones que se la haya atendido en un periodo de tiempo determinado.
SELECT cliente.*, count(*) as frecuencia
FROM ventas, cliente
WHERE (fecha BETWEEN '2020-04-15' AND '2020-05-20') AND ventas.cliente_id = cliente.cliente_id
GROUP BY cliente_id
ORDER BY frecuencia DESC
LIMIT 1;

# 2) Mostrar las ventas obtenidas con respecto al servicio de solo diagnósticos.
SELECT *
FROM ventas
WHERE (SELECT count(*) FROM servicio WHERE servicio.venta_id = ventas.venta_id) = 1
		AND (SELECT count(*) FROM servicio WHERE servicio.venta_id = ventas.venta_id AND servicio.nombre = 'Diagnostico') = 1;

# 3) Mostrar al empleado que mayor ganancia obtuvo en un rango de fechas determinado.
SELECT empleado.*, sum(total) as ganancia
FROM empleado, ventas
WHERE (ventas.fecha BETWEEN '2020-01-01' AND '2020-05-31') AND ventas.cajero_id = empleado.empleado_id
GROUP BY empleado_id
ORDER BY ganancia DESC
LIMIT 1;

# 4) Mostrar las reparaciones más concurrentes en base a un departamento especifico. 
SELECT departamento_id, nombre as reparaciones_mas_concurrentes
FROM (
    SELECT departamento_id, nombre
    FROM servicio
    ORDER BY nombre DESC) AS a
GROUP BY departamento_id
ORDER BY departamento_id;

# 5) Mostrar el departamento con mayor afluencia de reparaciones en un rango de fechas determinado.
SELECT departamento_id, count(departamento_id) as afluencia
FROM servicio
INNER JOIN ventas ON ventas.venta_id = servicio.venta_id
WHERE (fecha BETWEEN '2020-04-15' AND '2020-05-20')
GROUP BY departamento_id
HAVING count(departamento_id) > 1;

# 6) Mostrar el consumo de refacciones clasificado por departamento. 
SELECT nombre, descripcion, costo, tiempo, departamento_id, empleado_id,  COUNT(*) as consumo
FROM servicio
GROUP BY nombre, departamento_id
ORDER BY departamento_id ASC;

# 7) Mostrar las características de los automóviles registrados en un periodo de tiempo. 
SELECT carro.carro_id, carro.marca, carro.modelo, carro.ano, carro.color, carro.placas, ventas.fecha as entrada, carro.salida
FROM carro, ventas
WHERE (ventas.fecha BETWEEN '2020-04-15' AND '2020-05-20') AND ventas.cliente_id = carro.cliente_id
GROUP BY carro_id
ORDER BY ventas.fecha;


# 8) Mostrar el cliente con mayor gasto en reparaciones. 
SELECT cliente.*, sum(total) as gasto
FROM cliente, ventas
WHERE (ventas.fecha BETWEEN '2020-04-15' AND '2020-05-20') AND ventas.cliente_id = cliente.cliente_id
GROUP BY cliente_id
ORDER BY gasto DESC
LIMIT 1;

# 9) Mostrar cual es la refacción más vendida del departamento de refacciones y cuanto genera de ganancia. 
SELECT departamento_id, nombre as refaccion_mas_vendida, costo * (SELECT count(*) FROM servicio WHERE servicio.nombre = a.nombre) as ganancia
FROM (
    SELECT departamento_id, nombre, costo
    FROM servicio
    ORDER BY nombre DESC) AS a
GROUP BY departamento_id
ORDER BY departamento_id;

# 10) Mostrar los datos del trabajador con mayores garantías aplicadas de los servicios que realizo. 
SELECT DISTINCT empleado.*, (SELECT count(*) FROM servicio WHERE servicio.empleado_id = empleado.empleado_id AND servicio.garantia = 1) as garantias
FROM servicio
INNER JOIN empleado ON servicio.empleado_id = empleado.empleado_id
ORDER BY garantias DESC
LIMIT 1;
